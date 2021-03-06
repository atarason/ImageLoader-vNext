using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ImageLoader.Models.Contexts;

namespace ImageLoader.Migrations
{
    [DbContext(typeof(ImageLoaderDbContext))]
    partial class ImageLoaderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageLoader.Models.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Site")
                        .IsRequired()
                        .Annotation("MaxLength", 128);

                    b.Property<string>("URL")
                        .IsRequired();

                    b.Property<DateTimeOffset>("UpdatedDate");

                    b.HasKey("Id");
                });
        }
    }
}
