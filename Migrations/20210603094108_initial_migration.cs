using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simple_Book_Store.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 25, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    IsShipped = table.Column<bool>(nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    PriceOnSale = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsProductOfTheWeek = table.Column<bool>(nullable: false),
                    IsOnSale = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageThumbnailUrl = table.Column<string>(nullable: true),
                    IsOnFrontPage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "743d3d60-9850-4da7-9e1a-87fc021e31b8", "743d3d60-9850-4da7-9e1a-87fc021e31b8", "Admin", "ADMIN" },
                    { "02138091-57c5-40eb-92c3-33411e769430", "02138091-57c5-40eb-92c3-33411e769430", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressLine1", "AddressLine2", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "ZipCode" },
                values: new object[,]
                {
                    { "2c98f4be-a893-43d5-a7e5-830f72b8367e", 0, "Adminsgatan 42", "Plan 9 (from outer space)", "Adminköping", "e115a3f3-69e0-4300-8fad-a033aae2a8c5", "Adminarnia", "root@superuser.com", true, "Admin", "Adminsson", false, null, "ROOT@SUPERUSER.COM", "ROOT@SUPERUSER.COM", "AQAAAAEAACcQAAAAEAfX+lg0YeP6ZA1TYFtizVCCK1TzA0ERm8mffEExxtRwDAocqoxXwFDkoRy7h2ElfQ==", null, false, "e2cd21e0-5c5e-4b6b-a631-97137d99c70e", false, "root@superuser.com", "12345" },
                    { "e4a07e4f-af32-46b1-b7a5-098dff1da176", 0, "Black Castle", "(somewhere in the clouds)", "Adrilankha", "318c0efc-014a-4408-a748-8a1799a3ec40", "Dragaera", "vlad@taltos.com", true, "Vlad", "Taltos", false, null, "VLAD@TALTOS.COM", "VLAD@TALTOS.COM", "AQAAAAEAACcQAAAAEOFaBUQHoUgTQDaS9Yzw3eOOjpzfyWlcm8oqjPdlBwnB+m1HhfYe/Qz000Ifum7NuA==", null, false, "681f8968-5a1e-4691-b1ea-0d3e045c32ff", false, "vlad@taltos.com", "42222" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Adventure", "Exciting adventures with historical background" },
                    { 2, "Crime", "Dramatic detective stories" },
                    { 3, "Fantasy", "Get in the mood for some magic!" },
                    { 4, "Horror", "Thrilling horror stories" },
                    { 5, "Humour", "Fun, Comic, Farce, all you need is laugh!" },
                    { 6, "NonFiction", "Biographies, history and educational stuff" },
                    { 7, "Romance", "Sweet sensations and love" },
                    { 8, "SciFi", "Science and fiction, a good combo!" },
                    { 9, "Western", "Gunsmoke, heroes and villains" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "2c98f4be-a893-43d5-a7e5-830f72b8367e", "743d3d60-9850-4da7-9e1a-87fc021e31b8" },
                    { "e4a07e4f-af32-46b1-b7a5-098dff1da176", "02138091-57c5-40eb-92c3-33411e769430" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "CategoryId", "Description", "ImageThumbnailUrl", "ImageUrl", "IsOnFrontPage", "IsOnSale", "IsProductOfTheWeek", "Price", "PriceOnSale", "Title" },
                values: new object[,]
                {
                    { 1, "Dan Brown", 1, "A Robert Langdon adventure", "/images/Books_small/A1.jpg", "/images/Books/A1.jpg", false, false, false, 99.95m, 89.95m, "Da Vinci koden" },
                    { 2, "Frans G Bengtsson", 1, "Historic viking novel", "/images/Books_small/A2.jpg", "/images/Books/A2.jpg", false, true, false, 77.95m, 67.95m, "Röde orm" },
                    { 3, "Colin Dexter", 2, "A C Dexter crime novel", "/images/Books_small/C1.jpg", "/images/Books/C1.jpg", false, true, false, 77.95m, 67.95m, "Flicka försvunnen" },
                    { 4, "JK Rowling", 3, "JK Rowling magic", "/images/Books_small/F1.jpg", "/images/Books/F1.jpg", false, false, false, 88.95m, 78.95m, "Harry Potter och Dödsrelikerna" },
                    { 5, "David Seltzer", 4, "Horror by D Seltzer", "/images/Books_small/Ho1.jpg", "/images/Books/Ho1.jpg", false, false, false, 48.95m, 38.95m, "The Omen" },
                    { 6, "Tom Sharpe", 5, "Brilliant fun!", "/images/Books_small/Hu1.jpg", "/images/Books/Hu1.jpg", false, false, false, 68.95m, 59.95m, "The Wilt inheritance" },
                    { 7, "Terry Jones, Alan Ereira", 6, "Historic facts and myths", "/images/Books_small/NF1.jpg", "/images/Books/NF1.jpg", false, false, false, 99m, 89.95m, "Barbarerna" },
                    { 8, "Margaret George", 6, "A king's diary", "/images/Books_small/NF2.jpg", "/images/Books/NF2.jpg", false, false, false, 99.95m, 79.95m, "Henrik VIII:s Självbiografi" },
                    { 9, "Jan Skansholm", 6, "A C# programming language tutorial", "/images/Books_small/NF3.jpg", "/images/Books/NF3.jpg", false, false, false, 42.95m, 35.95m, "Skarp programmering med C#" },
                    { 10, "Judith Krantz", 7, "Love and romance", "/images/Books_small/R1.jpg", "/images/Books/R1.jpg", false, false, false, 88.95m, 78.95m, "Mistrals dotter" },
                    { 11, "Steven Brust", 8, "The first three Vlad Taltos novels", "/images/Books_small/SF1.jpg", "/images/Books/SF1.jpg", false, false, true, 99.95m, 79.95m, "The book of Jhereg" },
                    { 12, "Marshall Grover (L F Meares)", 9, "Klassiskt möte mellan hjältar!", "/images/Books_small/W1.jpg", "/images/Books/W1.jpg", false, false, false, 39.95m, 29.95m, "Bill & Ben - Big Jim" },
                    { 13, "Marshall Grover (L F Meares)", 9, "Humorfylld western klassiker", "/images/Books_small/W2.jpg", "/images/Books/W2.jpg", false, false, false, 39.95m, 29.95m, "Bill och Ben och Dakota Red" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
