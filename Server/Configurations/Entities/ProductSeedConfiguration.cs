//using System;
//using Microsoft.EntityFrameworkCore;
//using ProjectTech.Shared.Domain;

//namespace ProjectTech.Server.Configurations.Entities
//{
//    public class ProductSeedConfiguration : IEntityTypeConfiguration<Product>
//    {
//        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
//        {
//            builder.HasData(
//            new Product
//            {
//                ProductId = 1,
//                CustomerId = 1,
//                Price = "",
//                Name = "Administrator",
//                NormalizedName = "ADMINISTRATOR"
//            },

//             new Product
//             {
//                 ProductId = 2,
//                 Name = "User",
//                 NormalizedName = "USER"
//             }
// );
//        }
//    }
//}
