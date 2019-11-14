namespace BlogFall.Migrations
{
    using BlogFall.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogFall.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogFall.Models.ApplicationDbContext context)
        {
            #region Admin rolünü ve kullanýcýsýný oluþtur
            //foreach (var item in context.Users) //Bunu bi seferlik yaptýk tüm kullanýcýlar aktif oldu.
            //{
            //    item.IsEnabled = true;
            //}
            //return;
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);//Veritabani ile iliþkiyi saðlýyor.Bir managerin kendine veriði komutlarý uyguluyo.
                var manager = new RoleManager<IdentityRole>(store);//rolemanager class rolleri bulmada ve kullanmada iþimize yariyo.Arka planda storu kullanýyo.
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "ozknlimedine@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "ozknlimedine@gmail.com",
                    Email = "ozknlimedine@gmail.com" };//eðer yoksa bu kiþiyi oluþtur diyoruz.

                manager.Create(user, "Ankara1.");
                manager.AddToRole(user.Id, "Admin");//Admin olarak ekledik.

                #region Kategoriler ve yazýlar
                if (!context.Categories.Any())
                {
                    Category cat1 = new Category
                    {
                        CategoryName = "Gezi Yazýlarý"
                    };
                    cat1.Posts = new List<Post>();//Hashset te olur eðer öyle olursa olaný tekrar ekleyemeyiz.
                    cat1.Posts.Add(new Post
                    {
                        //Category id yazmadýk çünkü kendi yuklarda oluþtururken belirtiyo ve kendi altýna ekliyor.
                        Title = "Gezi Yazýsý 1",
                        AuthorId = user.Id,
                        Content = @"<p>
    Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>

<p>
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>
<p>
    Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.
</p>",
                        CreationTime = DateTime.Now
                    });

                    cat1.Posts.Add(new Post
                    {
                        Title = "Gezi Yazýsý 2",
                        AuthorId = user.Id,
                        Content = @"<p>
    Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>

<p>
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>
<p>
    Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.
</p>",
                        CreationTime = DateTime.Now
                    });

                    Category cat2 = new Category
                    {
                        CategoryName = "Ýþ Yazýlarý"
                    };
                    cat2.Posts = new List<Post>();
                    cat2.Posts.Add(new Post
                    {
                        Title = "Ýþ Yazýsý 1",
                        AuthorId = user.Id,
                        Content = @"<p>
    Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>

<p>
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>
<p>
    Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.
</p>",
                        CreationTime = DateTime.Now
                    });

                    cat2.Posts.Add(new Post
                    {
                        Title = "Ýþ Yazýsý 2",
                        AuthorId = user.Id,
                        Content = @"<p>
    Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>

<p>
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>
<p>
    Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.
</p>",
                        CreationTime = DateTime.Now
                    });
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                }
                #endregion

            }

            #endregion//ctrl+k+s

            #region Admin kullanýcýsýna 77 yeni yazý ekle
            if (!context.Categories.Any(x => x.CategoryName=="Diðer"))//ilk boþ olduðunda gircek bidaha girmeyecek.
            {
                ApplicationUser admin = context.Users.Where(x => x.UserName == "ozknlimedine@gmail.com").FirstOrDefault();
                if (admin != null)
                {
                    if (!context.Categories.Any(x => x.CategoryName == "Diðer"))
                    {
                        Category diger = new Category
                        {
                            CategoryName = "Diðer",
                            Posts = new HashSet<Post>()
                        };
                        for (int i = 1; i <= 77; i++)
                        {
                            diger.Posts.Add(new Post
                            {
                                //Category id yazmadýk çünkü kendi yuklarda oluþtururken belirtiyo ve kendi altýna ekliyor.
                                Title = "Diger Yazý " + i,
                                AuthorId = admin.Id,
                                Content = @"<p>
    Ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat.
    Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.
</p>
<p>
    Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.
</p>",
                                CreationTime = DateTime.Now.AddMinutes(i)
                            });
                        }
                        context.Categories.Add(diger);

                    }
                }
            }
            #endregion

        }
    }
}
