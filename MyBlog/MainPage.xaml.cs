using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace EFGetStarted.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        List<Blog> AllBlogs;
        List<Post> AllPosts;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new BloggingContext())
            {
                //AllPosts = db.Posts.ToList();
                AllBlogs = db.Blogs.ToList();
                BlogsList.ItemsSource = AllBlogs;
                AllPosts = db.Posts.ToList();
            }
        }

        private void AddBlog_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new BloggingContext())
            {
                var blog = new Blog { Url = NewBlogUrl.Text };
                db.Blogs.Add(blog);
                db.SaveChanges();

                //AllPosts = db.Posts.ToList();
                AllBlogs = db.Blogs.ToList();
                BlogsList.ItemsSource = AllBlogs;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string idStr = PostId.Text;
            int id;
            Blog lBlog;
            Post lPost;

            string content = (sender is Button) ? (string)((Button)sender).Content : "";
            if (content == "")
                return;
            switch (content)
            {
                case "Add Post to Blog":
                    if (BlogsList.SelectedIndex != -1)
                    {
                        lBlog = (Blog)BlogsList.SelectedItem;
                        using (var db = new BloggingContext())
                        {
                            var post = new Post
                            {
                                BlogId = lBlog.BlogId,
                                Title = NewPostTitle.Text,
                                Content = NewPostContent.Text
                            };

                            var res = db.Posts.Add(post);
                            if (res.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                            {
                                post = res.Entity;
                            }

                            int i = db.SaveChanges();

                            //AllPosts = db.Posts.ToList();
                            AllBlogs = db.Blogs.ToList();
                            BlogsList.ItemsSource = AllBlogs;
                            AllPosts = db.Posts.ToList();

                            PostId.Text = post.PostId.ToString();
                            NewPostBlog.Text = post.Blog.Url;


                            //Post newPost = (from p in AllPosts where p.PostId == post.PostId select p).FirstOrDefault();
                            //if (newPost != null)
                            //{
                            //    PostId.Text = newPost.PostId.ToString();
                            //    NewPostBlog.Text = newPost.Blog.Url;
                            //}
                        }

                    }
                    break;
                case "Prev Post":
                    if (int.TryParse(idStr, out id))
                    {
                        id--;

                        if (id > 0)
                        {
                            PostId.Text = id.ToString();

                            using (var db = new BloggingContext())
                            {
                                AllBlogs = db.Blogs.ToList();
                                AllPosts = db.Posts.ToList();
                                lPost = (from p in AllPosts where p.PostId == id select p).FirstOrDefault();
                                if (lPost != null)
                                {
                                    NewPostTitle.Text = lPost.Title;
                                    NewPostContent.Text = lPost.Content;
                                    NewPostBlog.Text = lPost.Blog.Url;
                                }
                                else
                                {
                                    NewPostTitle.Text = "No Post";
                                    NewPostContent.Text = "";
                                    NewPostBlog.Text = "";
                                }
                            }
                        }
                    }
                    break;
                case "Next Post":
                    if (int.TryParse(idStr, out id))
                    {
                        id++;
                        PostId.Text = id.ToString();
                        using (var db = new BloggingContext())
                        {
                            AllBlogs = db.Blogs.ToList();
                            AllPosts = db.Posts.ToList();
                            lPost = (from p in AllPosts where p.PostId == id select p).FirstOrDefault();
                            if (lPost != null)
                            {
                                NewPostTitle.Text = lPost.Title;
                                NewPostContent.Text = lPost.Content;
                                NewPostBlog.Text = lPost.Blog.Url;
                            }
                            else
                            {
                                NewPostTitle.Text = "No Post";
                                NewPostContent.Text = "";
                                NewPostBlog.Text = "";
                            }
                        }
                    }
                    break;
                case "Delete Selected Blog":
                    lBlog = (Blog)BlogsList.SelectedItem;
                    using (var db = new BloggingContext())
                    {


                        var res = db.Blogs.Remove(lBlog);

                        int i = db.SaveChanges();

                        AllBlogs = db.Blogs.ToList();
                        BlogsList.ItemsSource = AllBlogs;
                        AllPosts = db.Posts.ToList();

                        PostId.Text = "";
                        NewPostTitle.Text = "";
                        NewPostContent.Text = "";
                        NewPostBlog.Text = "";
                    }
                    break;
                case "Delete Current Post":
                    if (int.TryParse(idStr, out id))
                    {

                        if (id > 0)
                            using (var db = new BloggingContext())
                            {
                                lPost = (from p in AllPosts where p.PostId == id select p).FirstOrDefault();
                                if (lPost != null)
                                {

                                    var res = db.Posts.Remove(lPost);

                                    int i = db.SaveChanges();

                                    AllBlogs = db.Blogs.ToList();
                                    BlogsList.ItemsSource = AllBlogs;

                                    AllPosts = db.Posts.ToList();

                                    PostId.Text = "";
                                    NewPostTitle.Text = "";
                                    NewPostContent.Text = "";
                                    NewPostBlog.Text = "";
                                }
                            }
                        
                    }
                    break;
            }
        }

        private void Blogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BlogsList.SelectedIndex != -1)
            {

                using (var db = new BloggingContext())
                {
                    //AllBlogs = db.Blogs.ToList();
                    Blog Blog = (Blog)BlogsList.SelectedItem;
                    //Blog = (from b in AllBlogs where b.BlogId == Blog.BlogId select b).First();
                    if (Blog != null)
                    {
                        if (Blog.Posts != null)
                            if (Blog.Posts.Count != 0)
                            {
                                Post Post = Blog.Posts.First();
                                PostId.Text = Post.PostId.ToString();
                                NewPostTitle.Text = Post.Title;
                                NewPostContent.Text = Post.Content;
                                NewPostBlog.Text = Post.Blog.Url;
                            }
                    }
                }
            }
        }
    }
}