

#region using statements

using DataJuggler.Blazor.Components;
using DataJuggler.Blazor.Components.Interfaces;
using DataJuggler.PixelDatabase;
using DataJuggler.UltimateHelper;
using DataJuggler.UltimateHelper.Objects;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.Versioning;

#endregion

namespace NotJeopardy.Components.Pages
{

    #region class Home
    /// <summary>
    /// This class is the main page for this app
    /// </summary>
    
    [SupportedOSPlatform("windows")]
    public partial class Home : IBlazorComponentParent
    {
        
        #region Private Variables
        private ImageComponent blueImage;
        private ImageButton createImageButton;
        private TextBoxComponent promptTextBox;
        private LinkButton downloadButton;
        #endregion
        
        #region Events
            
        #endregion
        
        #region Methods
            
            #region ButtonClicked(int buttonNumber, string buttonText)
            /// <summary>
            /// method handles a button click
            /// </summary>
            public void ButtonClicked(int buttonNumber, string buttonText)
            {
                // if this is the first button (Create Image)
                if (buttonNumber == 1)
                {
                    // Create the text and image
                    CreateJeopardyText();
                }
            }
            #endregion           
            
            #region CreateJeopardyText()
            /// <summary>
            /// Create Jeopardy Text
            /// </summary>
            public void CreateJeopardyText()
            {
                // get the fullPath of the image
                string path = Path.Combine(Environment.CurrentDirectory, @"wwwroot\Images\Blue.png");

                // If the path Exists On Disk
                if ((FileHelper.Exists(path)) && (HasPromptTextBox) && (HasBlueImage))
                {
                    // Load the PixelDatabase
                    PixelDatabase pixelDatabase = PixelDatabaseLoader.LoadPixelDatabase(path, null);

                    // If the pixelDatabase object exists
                    if (NullHelper.Exists(pixelDatabase))
                    {
                        // Create a font
                        Font font = FontHelper.SearchForFont("ITC", 80);

                        // If the font object exists
                        if (font != null)
                        {
                            // Get the text
                            string text = PromptTextBox.Text;

                            // Convert the words into lines
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

                            // Create file name with a partial guid for uniqueness
                            string savedFilename = FileHelper.CreateFileNameWithPartialGuid(path, 12);

                            // Save the file
                            pixelDatabase.SaveAs(savedFilename);

                            // Get the name of the file
                            FileInfo fileInfo = new FileInfo(savedFilename);

                            // set the imageUrl
                            string imageUrl = "../Images/" + fileInfo.Name;

                            // Set the BlueImage
                            BlueImage.SetImageUrl(imageUrl);

                            // Show the component
                            BlueImage.SetVisible(true);

                            // if the value for HasDownloadButton is true
                            if (HasDownloadButton)
                            {
                                // Set the DownloadPath
                                DownloadButton.SetDownloadPath("Images/" + fileInfo.Name);

                                // Show the Download button
                                DownloadButton.SetVisible(true);
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region ReceiveData(Message message)
            /// <summary>
            /// This method is used to receive messages from other components or pages
            /// </summary>
            public void ReceiveData(Message message)
            {

            }
            #endregion
            
            #region Refresh()
            /// <summary>
            /// This method is used to update the UI after changes
            /// </summary>
            public void Refresh()
            {
                InvokeAsync(() =>
                {
                    // Refresh
                    StateHasChanged();
                });
            }
            #endregion
            
            #region Register(IBlazorComponent component)
            /// <summary>
            /// This method is used to store child controls that are on this component or page
            /// </summary>
            public void Register(IBlazorComponent component)            
            {
                if (component is ImageButton tempImageButton)
                {
                    // store the ImageButton component
                    if (component.Name == "CreateImageButton")
                    {
                        CreateImageButton = tempImageButton;
                    }
                }
                else if (component is ImageComponent tempImageComponent)
                {
                    // store the ImageComponent component
                    if (component.Name == "BlueImage")
                    {
                        BlueImage = tempImageComponent;
                    }
                }
                else if (component is LinkButton tempLinkButton)
                {
                    // store the LinkButton component
                    if (component.Name == "DownloadButton")
                    {
                        DownloadButton = tempLinkButton;
                    }
                }
                else if (component is TextBoxComponent tempTextBoxComponent)
                {
                    // store the TextBoxComponent component
                    if (component.Name == "PromptTextBox")
                    {
                        PromptTextBox = tempTextBoxComponent;
                    }
                }
            }
            #endregion

        #endregion

        #region Properties

            #region BlueImage               
            /// This property gets or sets the value for 'BlueImage'.
            /// </summary>
            public ImageComponent BlueImage
            {
                get { return blueImage; }
                set { blueImage = value; }
            }
            #endregion
            
            #region CreateImageButton
            /// <summary>
            /// This property gets or sets the value for 'CreateImageButton'.
            /// </summary>
            public ImageButton CreateImageButton
            {
                get { return createImageButton; }
                set { createImageButton = value; }
            }
            #endregion
            
            #region DownloadButton
            /// <summary>
            /// This property gets or sets the value for 'DownloadButton'.
            /// </summary>
            public LinkButton DownloadButton
            {
                get { return downloadButton; }
                set { downloadButton = value; }
            }
            #endregion
            
            #region HasBlueImage
            /// <summary>
            /// This property returns true if this object has a 'BlueImage'.
            /// </summary>
            public bool HasBlueImage
            {
                get
                {
                    // initial value
                    bool hasBlueImage = (BlueImage != null);

                    // return value
                    return hasBlueImage;
                }
            }
            #endregion
            
            #region HasCreateImageButton
            /// <summary>
            /// This property returns true if this object has a 'CreateImageButton'.
            /// </summary>
            public bool HasCreateImageButton
            {
                get
                {
                    // initial value
                    bool hasCreateImageButton = (CreateImageButton != null);

                    // return value
                    return hasCreateImageButton;
                }
            }
            #endregion
            
            #region HasDownloadButton
            /// <summary>
            /// This property returns true if this object has a 'DownloadButton'.
            /// </summary>
            public bool HasDownloadButton
            {
                get
                {
                    // initial value
                    bool hasDownloadButton = (DownloadButton != null);

                    // return value
                    return hasDownloadButton;
                }
            }
            #endregion
            
            #region HasPromptTextBox
            /// <summary>
            /// This property returns true if this object has a 'PromptTextBox'.
            /// </summary>
            public bool HasPromptTextBox
            {
                get
                {
                    // initial value
                    bool hasPromptTextBox = (PromptTextBox != null);

                    // return value
                    return hasPromptTextBox;
                }
            }
            #endregion
            
            #region PromptTextBox
            /// <summary>
            /// This property gets or sets the value for 'PromptTextBox'.
            /// </summary>
            public TextBoxComponent PromptTextBox
            {
                get { return promptTextBox; }
                set { promptTextBox = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}