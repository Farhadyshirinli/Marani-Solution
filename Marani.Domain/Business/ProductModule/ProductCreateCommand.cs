using Marani.Domain.AppCode.Extensions;
using Marani.Domain.Models.DataContexts;
using Marani.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marani.Domain.Business.ProductModule
{
    public class ProductCreateCommand : IRequest<Product>
    {

        [Required]
        public string Name { get; set; }

        [Required]

        public string StockKeepingUnit { get; set; }

        [Required]

        public decimal Price { get; set; }

        [Required]

        public string ShortDescription { get; set; }

        [Required]

        public string Description { get; set; }


        public int BrandId { get; set; }


        public int CategoryId { get; set; }

        //public int[] ItemIds { get; set; }
        [Required]

        public int ColorId { get; set; }
        [Required]

        public int RegionId { get; set; }
        [Required]
        public ImageItem[] Images { get; set; }


        public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
        {
            private readonly MaraniDbContext db;
            private readonly IHostEnvironment env;

            public ProductCreateCommandHandler(MaraniDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }

            public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var model = new Product();
                    model.ProductCatalog = new List<ProductCatalogItem>();
                    model.Name = request.Name;
                    model.StockKeepingUnit = request.StockKeepingUnit;
                    model.Price = request.Price;
                    model.ShortDescription = request.ShortDescription;
                    model.Description = request.Description;
                    model.BrandId = request.BrandId;
                    model.CategoryId = request.CategoryId;

                    if (request.Images != null && request.Images.Where(i => i.File != null).Count() > 0)
                    {
                        model.ProductImages = new List<ProductImages>();


                        foreach (var item in request.Images.Where(i => i.File != null))
                        {
                            var image = new ProductImages();
                            image.IsMain = item.IsMain;

                            string extension = Path.GetExtension(item.File.FileName);//.jpg

                            image.Name = $"product-{Guid.NewGuid().ToString().ToLower()}{extension}";

                            string fullName = env.GetImagePhysicalPath(image.Name);

                            using (var fs = new FileStream(fullName, FileMode.Create, FileAccess.Write))
                            {
                                await item.File.CopyToAsync(fs, cancellationToken);
                            }

                            model.ProductImages.Add(image);
                        }
                    }

                   

                    var itemIn = new ProductCatalogItem();
                    itemIn.ProductColorId = request.ColorId;
                    itemIn.ProductRegionId = request.RegionId;

                    model.ProductCatalog.Add(itemIn);

                    await db.Products.AddAsync(model, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);


                    return model;

                }
                catch (System.Exception)
                {
                    return null;
                }

            }
        }
    }
}
