# Not Jeopardy

This project is a demo of four opensource projects that will demonstrate how easy it is to build
Blazor projecs with DataJuggler.Blazor.Components. The output of this project is an image 
like you see below (if viewing on GitHub)

<img src="https://raw.githubusercontent.com/DataJuggler/SharedRepo/master/Shared/Images/BlazorJeopardy.png"
     width="455" height="270" alt="Blazor Jeopardy" />

This project creates images like the blue questions on Jeopardy. The main purpose is to show how
easy setting up DataJuggler.Blazor.Components is, and writing text to images using 
NuGet package DataJuggler.PixelDatabase.

# Cloning And Running This Project

There are two branches to this project, Main and Tutorial. If you just want to run the project, clone the 
Main branch and start debugging or F5 to run it. Type in some text and click Create and it will create the image. 
After the image is created, you can download the image with an example of the LinkButton will become visible.

What you will need: 

# Visual Studio 2022 if you want to run or follow the tutorial

This project should run on VS 2026 I just haven't tested it yet. 

# Regionizer 2022 If You Want To Walk Throught The Tutorial
https://github.com/DataJuggler/Regionizer2022

There is a VSIX package located in Regionizer2022\Regionizer\Install\Regionizer2022.vsix
Double Click On this VSIX to install Regionizer into Visual Studio 2022. To launch it after install,
under the tools menu you will see a menu icon for Regionizer2022. I dock Regioniozer in the same
panel as Solution Explorer, Properties, etc. 

Even if you hate regions, you will like the new features to create interfaces and wire up DataJuggler.Blazor.Components.

# ITC Korrina Font

To create images that look like the Jeopardy Questions, install this font.
https://font.download/font/itc-korinna-std

On the right hand side click Download for Free. Save the zip file to your hard drive, extract the zip file
and double click the font ITC Korinna Bold.otf, or you can use any font you want.

If you want to use another font see the Customizations section later in this document

# Cloning Main Brach

# ✅ Option 1 — Clone Through Visual Studio

Open Visual Studio 2022 (or VS 2026 should work)

Click Clone a repository

Enter this URL:

    https://github.com/DataJuggler/NotJeopardy.git

Choose your local folder

Click Clone

# 💻 Option 2 — Clone Using the Command Line

Open a command window

Press Win + R, type cmd, and press Enter

Navigate to the folder where you want the project
Example:
cd "C:\Projects"
or
cd "C:\Projects\GitHub"

Clone the repo

    git clone --branch main https://github.com/DataJuggler/NotJeopardy.git

Full example (if you have these folders)

cd "C:\Projects\GitHub"

    git clone --branch main https://github.com/DataJuggler/NotJeopardy.git"

After cloning, open NotJeopardy.sln in Visual Studio.

Press F5 to Start Debugging

# Tutorial

# Step 1: Add two NuGet packages
 
DataJuggler.Blazor.Components
DataJuggler.PixelDatabase

# Step 2: Setup Program.cs

Open Program.cs and add the following code

BlazorStyled is used for dynamic CSS styling

    using BlazorStyled;

Around line 16, add this line to register BlazorStyled

    // Required
    builder.Services.AddBlazorStyled();

# Step 3: Setup App.razor

Add the following links to App.razor to link to the CSS class in DataJuggler.Blazor.Components

Directly above <ImportMap /> add

    <link href="/_content/DataJuggler.Blazor.Components/css/DataJuggler.Blazor.Components.css" rel="stylesheet" />
    <link rel="icon" type="image/x-icon" href="favicon.ico" />

Directly above </body> add this link to the JavaScript file. This makes the LinkButton able to download the images

     <script src="_content/DataJuggler.Blazor.Components/_content/Blazor.JavaScriptInterop/BlazorJSInterop.js"></script>

After Step 3 you have DataJuggler.Blazor.Components setup for this project. We are now ready to build the 
Home.razor and Home.razor.cs

# Step 4 Modify Home.razor

Erase everything in Home.razor except for the top page directive. Home.razor is located in Components\Pages\

Add the following two lines directly below @page "/"

    @using DataJuggler.Blazor.Components
    @using System.Drawing;

Add the following markup. This will add a TextBoxComponent and an ImageButton.

    <div class="dislayinline">
        <TextBoxComponent Name="PromptTextBox" Unit="px" HeightUnit="px" Parent="this"
            Caption="Text:" Width="480" Multiline="true" Height="128" TextBoxClassName="height116"
            Column1Width="80" LabelWidth="64" LabelLeft="32" LabelTextAlign="right"
            LabelFontSize="16" LabelFontName="Calibri" LabelClassName="right20 up108">
        </TextBoxComponent>
        <ImageButton Name="CreateImageButton" Parent="this" ButtonNumber="1"
            ClickHandler=@ButtonClicked Height="60" Width="120"
            ImageUrl="../Images/BlackButton.jpg" TextColor=@Color.White
            Left="96" Top="0" Text="Create" FontName="Calibri" FontSize="20">
        </ImageButton>
    </div>

Directly below the div, add this markup. This will add the ImageComponent and a LinkButton

    <ImageComponent Name="BlueImage" Parent="this" TextAlign="Center"
        Height="270" Width="480" ImageUrl="../Images/Blue.png"
        Left="0" Top="0" Visible="false">
    </ImageComponent>
    <LinkButton Name="DownloadButton" Parent="this" TextColor="@Color.MediumBlue"
        HideOnDownload="true" Text="Download" Left="-16" FontSize=18
        Top="-8" Visible="false" ButtonNumber="2">
    </LinkButton>

Save Home.razor

# Step 6 (Almost Done!) Setup Home.razor.cs

Select the Pages folder under components, right click -> Select Add Class -> 
Name the class Home.razor.cs

A new class will be created. You will see a squiggly line under 

# Customizations

This was just a short demo. If you wanted to use this for other images, the section below describes
how to use another, font or text color.

# Using another font

If you want to use another font, choose a font on your system and search in the ClickHandler method of
Home.razor.cs for the line FontHelper.SearchForFont, this method will search your hard drive for a font that
starts with whatever text, or create your own font manually.

    // Create a font
    Font font = FontHelper.SearchForFont("ITC", 80);

Change the search text from ITC to a font on your system. Fonts are either in C:\Windows\Fonts
or C:\Users\<You>\AppData\Local\Microsoft\Windows\Fonts, where <You> is your Windows user name.

# Using Another Image

There is a blank Blue image, Blue.png, located in wwwroot\Images folder. The PixelDatabase is loaded with the
blue image. This code snippet below comes from the CreateJeopardyImage method. Change the path 
below to load another image if you wanted to modify this app.

    // get the fullPath of the image
    string path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\Images\Blue.png");

    // If the path Exists On Disk
    if ((FileHelper.Exists(path)) && (HasPromptTextBox) && (HasBlueImage))
    {
        // Load the PixelDatabase
        PixelDatabase pixelDatabase = PixelDatabaseLoader.LoadPixelDatabase(path, null);

# Changing Text Color
    
    PixelDatabase.DrawText method accepts a parameter Brushes.(Color) 

    // Get the text
    string text = PromptTextBox.Text;

    // Convert the text into lines
    List<TextLine> lines = TextHelper.SplitTextIntoLines(text, 1080, 24, 128);

    // if there are one or more lines
    if (ListHelper.HasOneOrMoreItems(lines))
    {
        // iterate the lines
        foreach(TextLine line in lines)
        {  
            // Draw the text
            pixelDatabase.DrawText(line.Text, font, new Point(960, line.Top), StringAlignment.Center, StringAlignment.Center, Brushes.White);
        }
    }

# Different Size Images

When the text is split into a list of TextLine objects, the second parameter is the image height. 
The 3rd parameter is the characters per line to split on and the 4th parameter is the height per line.
This is used to vertically center the text. Each TextLine object is returned with a line.Top set.

    // Convert the text into lines
    List<TextLine> lines = TextHelper.SplitTextIntoLines(text, 1080, 24, 128);

When drawing a TextLine, the the 3rd parameter creates a new Point object. The width of that point
should be half your image width. The Blue.png image is 1920 x 1080, so 960 is the mid point.

    // Draw the text
    pixelDatabase.DrawText(line.Text, font, new Point(960, line.Top), StringAlignment.Center, StringAlignment.Center, Brushes.White);