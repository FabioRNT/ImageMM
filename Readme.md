**Application Description:**

The ImageMM application is capable of loading common images, e.g., *.png, *.jpg, and calculating the average value of each of the RGB¹ (Red, Green, Blue) and YCbCr² (Luma, Chroma blue, Chroma red) color spaces.

This application also provides a set of filters, presented in the right dropdown list, which allow the users to select any of the given color spaces and the image will show only the selected channels. To do so, this application loads the image into a bitmap and then uses the AForge.NET³ framework, which allows the removal of color channels from the given bitmap.

In addition, this application has a simple zooming tool. You can use the zoom tool by clicking and dragging in the image and zoom out by clicking on the button.

This application was developed and presented to fulfill an assignment of the multimedia applications course, lectured by Dr. Celso Alberto Saibel Santos at the Federal University of Espírito Santo during the 2nd semester of 2016.

**Known Issues:**

* If you try to apply a filter to a zoomed image, the filter will be applied, but the image will return to its original size.
* The zooming tool did not scale properly with the image dimensions.

**Requirements:**

* To run the application: .NET Framework version 4.5.2, run WindowsApplication.exe at bin\Release\
* To edit the application: Visual Studio 2017 (Not checked in older versions), import ImageMultimedia.sln to edit the application.

¹ See https://en.wikipedia.org/wiki/RGB_color_space for detailed info.

² See https://en.wikipedia.org/wiki/YCbCr for detailed info.

³ See the documentation of the AForge.NET framework in: http://www.aforgenet.com/framework/
