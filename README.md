# DJsEFBlogExample
A Sqlite Entity Framework UWP Sample app as per MS Docs

Following the first part of Getting Started with EF Core on Universal Windows Platform (UWP) with a New Database from Microsoft docs.

Also using my hints in Entity Framework with UWP and SQLite: Referencing the Model

- Using Visual Studio 15.5.7
- Using EF Version 2.1.1 in what follows.

There is an issue with respect to referencing an EF Framework Core class library from a UWP app. This solution has resolved the issue.

# Branches
Apart from the Master, the other branches are snapshots of the Master at various stages.

## Master
This is the main sequence, latest version is here.

This starts with the example in Microsoft Docs example at [https://docs.microsoft.com/en-us/ef/core/get-started/uwp/getting-started] entitled: **"Getting Started with EF Core on Universal Windows Platform (UWP) with a New Database"**. That only implements adding Blogs, no Posts created. The app is then implements Posts creation and some CRUD, database manipulations.

## 1. MSDocsExample
Steps as per, and only as per, the Microsoft Docs example with a few small refinemnets

## 2. AddPostsAndCRUD
Can add posts to a Blog. Also implements CRUD besides CREATE (Lblogs and Posts) can READ (query to get Posts for a Blog), can UPDATE Blog and Post, and DELETE (can delete indivual Posts or a Blog and all of of its Posts.

