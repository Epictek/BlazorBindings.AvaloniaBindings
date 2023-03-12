namespace ControlGallery.Views.Collections.CollectionView.DynamicItems;

public record ItemModel;
public record TextItemModel(string Text) : ItemModel;
public record ImageItemModel(ImageSource ImageSource) : ItemModel;

public static class Items
{
    public static ItemModel[] GetItems()
    {
        var items = new ItemModel[] {
            new ImageItemModel("https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885_960_720.jpg"),
            new TextItemModel(".NET Multi-platform App UI (.NET MAUI) is a cross-platform framework for creating mobile and desktop apps with C# and XAML. Using .NET MAUI, you can develop apps that can run on Android, iOS, iPadOS, macOS, and Windows from a single shared codebase."),
            new ImageItemModel("https://cdn.pixabay.com/photo/2014/02/27/16/10/flowers-276014_960_720.jpg"),
            new TextItemModel("ASP.NET Core is a cross-platform .NET framework for building modern cloud-based web applications on Windows, Mac, or Linux."),
            new ImageItemModel("https://cdn.pixabay.com/photo/2015/06/19/21/24/avenue-815297_960_720.jpg"),
            new ImageItemModel("https://cdn.pixabay.com/photo/2013/10/02/23/03/mountains-190055_960_720.jpg"),
            new TextItemModel(".NET is a cross-platform runtime for cloud, mobile, desktop, and IoT apps."),
            new TextItemModel("Visual Studio Code is a code editor redefined and optimized for building and debugging modern web and cloud applications. ")
        };

        var repeatedItems = Enumerable.Repeat(items, 100).SelectMany(i => i).ToArray();

        return repeatedItems;
    }
}
