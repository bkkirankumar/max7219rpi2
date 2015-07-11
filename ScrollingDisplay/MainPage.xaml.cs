/*
Author   : KIRAN KUMAR BOLLUKONDA
Email    : BK.KIRAN.KUMAR@GMAIL.COM
           BK.KIRAN.KUMAR@HOTMAIL.COM
Created  : 21-JUNE-2015

Comments : Please note that this code is written only for beginners 
           and I tried to keep as simple as possible. This code allows 
           the beginner to understand the MAX7219 via Raspberry Pi 2 
           SPI with Windows 10.
*/

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
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;
using Windows.Devices.Gpio;
using System.Threading.Tasks;


namespace ScrollingDisplay
{
    public sealed partial class MainPage : Page
    {
        private const string SPI_CONTROLLER_NAME = "SPI0";  // Use SPI0.
        private const Int32 SPI_CHIP_SELECT_LINE = 0;       // Line 0 is the CS0 pin which is 
                                                            // the physical pin 24 on the Rpi2.

        
        // private const UInt32 MAX_DISPLAYS = 1;
        // private const UInt32 DISPLAY_ROWS = 8;
        private const UInt32 DISPLAY_COLUMNS = 8;
        // private byte[] DisplayBuffer = new byte[DISPLAY_COLUMNS * 2]; // Including address byte. 1 Address byte, 1 Data byte.

        private string Message;                             // Message to be displayed on the LED display.
        private byte[] MessageBuffer;                       // Message Buffer to hold total character font.
        private int ScrollDelay = 40;                       // In milliseconds. Use this field to change dynamically for scroll speed.
        private byte[] SendBytes = new byte[2];             // Send to Spi Display without drawing memory.

        // COMMAND MODES for MAX7219. Refer to the table in the datasheet.
        private static readonly byte[] MODE_DECODE      = { 0x09, 0x00 }; // , 0x09, 0x00 };
        private static readonly byte[] MODE_INTENSITY   = { 0x0A, 0x00 }; // , 0x0A, 0x00 };
        private static readonly byte[] MODE_SCAN_LIMIT  = { 0x0B, 0x07 }; // , 0x0B, 0x07 };
        private static readonly byte[] MODE_POWER       = { 0x0C, 0x01 }; // , 0x0C, 0x01 };
        private static readonly byte[] MODE_TEST        = { 0x0F, 0x00 }; // , 0x0F, 0x00 };
        private static readonly byte[] MODE_NOOP        = { 0x00, 0x00 }; // , 0x00, 0x00 };

        private SpiDevice SpiDisplay;                   // SPI device on Raspberry Pi 2
        private GpioController IoController;            // GPIO Controller on Raspberry Pi 2

        private int uCtr, rCtr;     // Counter variables for updating message.

        public MainPage()
        {
            this.InitializeComponent();
            // Initialize Scrolling Message
            InitScrollMessage(); // You can override the default message by providing some string here. 
            Initialize();        // Initialize SPI and GPIO on the current system.
        }

        /// <summary>
        /// Initialize Scroll Message.
        /// </summary>
        /// <param name="msg">Message to scroll.</param>
        private void InitScrollMessage(string msg = "KIRAN KUMAR BOLLUKONDA * ")
        {
            Message = msg;                                  // Scroll Message.
            MessageBuffer = new byte[Message.Length * 8];   // Message Buffer containing the character font.

            // Fill message buffer with the character font.
            for (int i = 0; i < Message.Length; i++)
            {
                Array.Copy(Character.Get(Message[i]),   // Source array.
                                    0,                  // Source start index.
                                    MessageBuffer,      // Destination array.
                                    (i * 8),            // Destination array index.
                                    8);                 // Length of bytes to copy.
            }
        }

        /// <summary>
        /// Initialize SPI, GPIO and LED Display
        /// </summary>
        private async void Initialize()
        {
            try
            {
                InitGpio();            
                await InitSpi();       
                await InitDisplay();
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Error Occurred: \r\n" + ex.Message;
                if (ex.InnerException != null)
                {
                    txtStatus.Text += "\r\nInner Exception: " + ex.InnerException.Message;
                }
                return;
            }
        }

        /// <summary>
        /// Initialize SPI.
        /// </summary>
        /// <returns></returns>
        private async Task InitSpi()
        {
            try
            {
                var settings = new SpiConnectionSettings(SPI_CHIP_SELECT_LINE);
                settings.ClockFrequency = 100000;
                settings.Mode = SpiMode.Mode0;

                string spiAqs = SpiDevice.GetDeviceSelector(SPI_CONTROLLER_NAME);       /* Find the selector string for the SPI bus controller          */
                var devicesInfo = await DeviceInformation.FindAllAsync(spiAqs);         /* Find the SPI bus controller device with our selector string  */
                SpiDisplay = await SpiDevice.FromIdAsync(devicesInfo[0].Id, settings);  /* Create an SpiDevice with our bus controller and SPI settings */
            }
            /* If initialization fails, display the exception and stop running */
            catch (Exception ex)
            {
                throw new Exception("SPI Initialization Failed", ex);
            }
        }

        /// <summary>
        /// Initialize LED Display. Refer to the datasheet of MAX7219
        /// </summary>
        /// <returns></returns>
        private async Task InitDisplay()
        {
            SpiDisplay.Write(MODE_SCAN_LIMIT);
            await Task.Delay(10);
            SpiDisplay.Write(MODE_INTENSITY);
            await Task.Delay(10);
            SpiDisplay.Write(MODE_POWER);
            await Task.Delay(10);
            SpiDisplay.Write(MODE_TEST); // Turn on all LEDs.
            await Task.Delay(10);
        }

        /// <summary>
        /// Initiazlie GPIO.
        /// </summary>
        private void InitGpio()
        {
            IoController = GpioController.GetDefault(); 
            if (IoController == null)
            {
                throw new Exception("Unable to find GPIO on the current system.");
            }
        }

        // Use the below method to debug or test if the font character is correct.
        //private async void DisplayChar(char ch)
        //{
        //    byte[] c = Character.Get(ch);
        //    for (int i = 0; i < 8; i++)
        //    {
        //        SpiDisplay.Write(new byte[] { (byte)(i + 1), c[i] });
        //        await Task.Delay(10);
        //    }
        //}

        
        /// <summary>
        /// Update Message to LED display.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateMessage()
        {
            for (uCtr = 0; uCtr < 8; uCtr++)
            {
                SendBytes[0] = (byte)(uCtr + 1); // Address
                SendBytes[1] = MessageBuffer[uCtr];
                // SpiDisplay.Write(new byte[] { (byte)(uCtr + 1), MessageBuffer[uCtr] });
                SpiDisplay.Write(SendBytes);
            }
            await Task.Delay(ScrollDelay);
        }

        /// <summary>
        /// Rotate message by one column in the message buffer.
        /// </summary>
        private void RotateDisplay()
        {
            byte tmpByte = MessageBuffer[0];
            for (rCtr = 1; rCtr < MessageBuffer.Length; rCtr++)
            {
                MessageBuffer[rCtr - 1] = MessageBuffer[rCtr];
            }
            MessageBuffer[MessageBuffer.Length - 1] = tmpByte;
        }

        /// <summary>
        /// Start scrolling forever.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnScroll_Click(object sender, RoutedEventArgs e)
        {
            btnScroll.IsEnabled = false;
            while (true)
            {
                await UpdateMessage();  //  Update message in LED display.
                RotateDisplay();        // Rotate message in buffer.
            }
        }
    }
}
