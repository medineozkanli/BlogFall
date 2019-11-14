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
            #region Admin rol�n� ve kullan�c�s�n� olu�tur
            //foreach (var item in context.Users) //Bunu bi seferlik yapt�k t�m kullan�c�lar aktif oldu.
            //{
            //    item.IsEnabled = true;
            //}
            //return;
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);//Veritabani ile ili�kiyi sa�l�yor.Bir managerin kendine veri�i komutlar� uyguluyo.
                var manager = new RoleManager<IdentityRole>(store);//rolemanager class rolleri bulmada ve kullanmada i�imize yariyo.Arka planda storu kullan�yo.
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "ozknlimedine@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "ozknlimedine@gmail.com",
                    Email = "ozknlimedine@gmail.com" };//e�er yoksa bu ki�iyi olu�tur diyoruz.

                manager.Create(user, "Ankara1.");
                manager.AddToRole(user.Id, "Admin");//Admin olarak ekledik.

                #region Kategoriler ve yaz�lar
                if (!context.Categories.Any())
                {
                    Category cat1 = new Category
                    {
                        CategoryName = "Gezi Yaz�lar�"
                    };
                    cat1.Posts = new List<Post>();//Hashset te olur e�er �yle olursa olan� tekrar ekleyemeyiz.
                    cat1.Posts.Add(new Post
                    {
                        //Category id yazmad�k ��nk� kendi yuklarda olu�tururken belirtiyo ve kendi alt�na ekliyor.
                        Title = "Gezi Yaz�s� 1",
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
                        Title = "Gezi Yaz�s� 2",
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
                        CategoryName = "�� Yaz�lar�"
                    };
                    cat2.Posts = new List<Post>();
                    cat2.Posts.Add(new Post
                    {
                        Title = "�� Yaz�s� 1",
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
                        Title = "�� Yaz�s� 2",
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

            #region Admin kullan�c�s�na 77 yeni yaz� ekle
            if (!context.Categories.Any(x => x.CategoryName=="Di�er"))//ilk bo� oldu�unda gircek bidaha girmeyecek.
            {
                ApplicationUser admin = context.Users.Where(x => x.UserName == "ozknlimedine@gmail.com").FirstOrDefault();
                if (admin != null)
                {
                    if (!context.Categories.Any(x => x.CategoryName == "Di�er"))
                    {
                        Category diger = new Category
                        {
                            CategoryName = "Di�er",
                            Posts = new HashSet<Post>()
                        };
                        for (int i = 1; i <= 77; i++)
                        {
                            diger.Posts.Add(new Post
                            {
                                //Category id yazmad�k ��nk� kendi yuklarda olu�tururken belirtiyo ve kendi alt�na ekliyor.
                                Title = "Diger Yaz� " + i,
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
