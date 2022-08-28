using KeyFunctions.Common.Enums;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctionsWinfowsFormsApp.Services
{
    public class ClipboardService
    {
        public static void CreateImagesDirectory()
        {
            Directory.CreateDirectory("./images");
        }

        public static ClipDataType GetDataType()
        {
            if (Clipboard.ContainsText())
            {
                return ClipDataType.Text;
            }
            else if (Clipboard.ContainsImage())
            {
                return ClipDataType.Image;
            }
            else
            {
                return ClipDataType.Unhandled;
            }
        }

        public static bool TryGetImage()
        {
            if (Clipboard.ContainsImage())
            {
                Image clipImage = Clipboard.GetImage();
                var base64String = GetBase64(clipImage);
            }
            return true;
        }

        public static void CopyImageToClip()
        {
            Image imageToCopy = Image.FromFile("./asdf.png");
            //Clipboard.SetDataObject(imageToCopy, true, 4, 15);
            Clipboard.Clear();
            Clipboard.SetImage(imageToCopy);
        }

        public static void TrySaveRawImageData()
        {
            var dataobject = Clipboard.GetDataObject();
            object asdf = dataobject.GetData(typeof(byte[]));
            var asdf2 = asdf as byte[];
        }

        public static bool TrySaveClipboardImageAndGetFileName(out string filename)
        {
            if (Clipboard.GetImage() is Image clipImage)
            {
                filename = Guid.NewGuid().ToString() + ".png";
                clipImage.Save($"./images/{filename}", ImageFormat.Png);
                return true;
            }
            filename = null;
            return false;
        }

        public static void CopyImageToClipFromTextFile()
        {
            //using Stream stream = new FileStream("./asdf.png.txt", FileMode.Open);
            string sigBase64 = File.ReadAllText("./asdf.png.txt");
            byte[] ms = Convert.FromBase64String(sigBase64);
            using Stream stream = new MemoryStream(ms);
            Image imageToCopy = Image.FromStream(stream);
            Clipboard.SetImage(imageToCopy);
        }

        public static void CopyImageToClipFromByteFile()
        {
            byte[] bytes = File.ReadAllBytes("./asdf.png.bytes.txt");
            Image imageToCopy;
            using (Stream stream = new MemoryStream(bytes))
            {
                imageToCopy = Image.FromStream(stream);
            }
            Clipboard.SetImage(imageToCopy);
        }

        public static string GetText()
        {
            return Clipboard.GetText(TextDataFormat.Text);
        }

        public static void SetText(string clipboardText)
        {
            Clipboard.SetDataObject(clipboardText, true, 5, 10);
        }

        public static void SaveImageAsDibBytes()
        {
            if (Clipboard.GetDataObject() is not IDataObject dataObject || (!dataObject.GetDataPresent(DataFormats.Dib)))
            {
                return;
            }
            var formatss = dataObject.GetFormats();

            var bitm = dataObject.GetData(DataFormats.Bitmap, false);

            using var dib = dataObject.GetData(DataFormats.Dib, false) as MemoryStream;
            File.WriteAllBytes($"./images/dibfalse.dat", dib.ToArray());

            var filedrop = Clipboard.GetFileDropList();


            object clipData = Clipboard.GetData(DataFormats.Dib);

            byte[] clipDataBytes = ((MemoryStream)clipData).ToArray();

            //var filename = Guid.NewGuid().ToString() + ".dat";
            var filename = "rrrrr.dat";
            File.WriteAllBytes($"./images/{filename}", clipDataBytes);
        }

        public static void CopyImageFromDibFalseBytesFile()
        {
            byte[] dibBytes = File.ReadAllBytes("./images/dibfalse.dat");
            var stream = new MemoryStream(dibBytes);
            Clipboard.SetData(DataFormats.Dib, stream);
        }

        public static void CopyImageFromDibBytesFile()
        {
            byte[] dibBytes = File.ReadAllBytes("./images/rrrrr.dat");

            var stream = new MemoryStream(dibBytes);

            Clipboard.SetData(DataFormats.Dib, stream);

            return;

            if (Clipboard.GetDataObject() is not IDataObject dataObject || (!dataObject.GetDataPresent(DataFormats.Dib)))
            {
                return;
            }


            byte[] clipDataBytes = ((MemoryStream)Clipboard.GetData(DataFormats.Dib)).ToArray();

            //var filename = Guid.NewGuid().ToString() + ".dat";
            var filename = "rrrrr.dat";
            File.WriteAllBytes($"./images/{filename}", clipDataBytes);
        }

        public static void GetImageFromClipboard()
        {
            if (Clipboard.GetDataObject() == null)
            {
                return;
            }
            Bitmap bitmap;
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Dib))
            {
                var dib = ((System.IO.MemoryStream)Clipboard.GetData(DataFormats.Dib)).ToArray();
                var width = BitConverter.ToInt32(dib, 4);
                var height = BitConverter.ToInt32(dib, 8);
                var bpp = BitConverter.ToInt16(dib, 14);
                if (bpp == 32)
                {
                    var gch = GCHandle.Alloc(dib, GCHandleType.Pinned);
                    Bitmap bmp = null;
                    try
                    {
                        var ptr = new IntPtr((long)gch.AddrOfPinnedObject() + 40);
                        bmp = new Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, ptr);
                        bitmap = new Bitmap(bmp);

                        bitmap.Save("./images/extrawork.png", ImageFormat.Png);
                    }
                    finally
                    {
                        gch.Free();
                        if (bmp != null) bmp.Dispose();
                    }
                }
            }



            //return Clipboard.ContainsImage() ? Clipboard.GetImage() : null;
        }

        #region PRIVATE METHODS

        private static string GetBase64(Image image)
        {
            //if ((DateTime.Now - lastimageTime).TotalSeconds < 2 || image == null)
            //{
            //    return null;
            //}
            // source links:
            // https://stackoverflow.com/questions/44287407/text-erased-from-screenshot-after-using-clipboard-getimage-on-windows-10/46400011#46400011
            // https://stackoverflow.com/questions/44177115/copying-from-and-to-clipboard-loses-image-transparency/46424800#46424800
            // https://stackoverflow.com/questions/59180953/adding-image-with-url-to-clipboard
            // https://stackoverflow.com/questions/4736155/how-do-i-convert-struct-system-byte-byte-to-a-system-io-stream-object-in-c
            // https://stackoverflow.com/questions/16072709/converting-string-to-byte-array-in-c-sharp
            // https://stackoverflow.com/questions/10889764/how-to-convert-bitmap-to-a-base64-string
            // https://stackoverflow.com/questions/5707990/requested-clipboard-operation-did-not-succeed
            // https://stackoverflow.com/a/46424800/8075004
            // 

            using var ms = new MemoryStream();
            //image.Save(ms, image.RawFormat);
            image.Save(ms, ImageFormat.Bmp);
            //image.Save("./asdf.png", ImageFormat.Png);

            byte[] imageBytes = ms.ToArray();
            //string sigBase64 = Convert.ToBase64String(imageBytes);

            File.WriteAllBytes("asdf.png.bytes.txt", imageBytes);

            //Clipboard.SetDataObject(sigBase64, true, 4, 15);

            //Thread.Sleep(50);
            //Clipboard.Clear();

            //byte[] imageBytes2 = Convert.FromBase64String(sigBase64);

            //using Stream stream = new MemoryStream(imageBytes2);

            //Image imageToCopy = Image.FromStream(stream);
            //Image imageToCopy = Image.FromFile("./asdf.png");

            //Clipboard.SetDataObject(imageToCopy, true, 4, 15);
            //Clipboard.SetData(DataFormats.Bitmap, imageToCopy);

            //IDataObject obj = new DataObject();
            //// As standard bitmap
            //obj.SetData(DataFormats.Bitmap, imageToCopy);

            //Clipboard.SetDataObject(imageToCopy, true, 4, 15);
            //Clipboard.SetImage(imageToCopy);

            return null;
        }

        #endregion
    }
}
