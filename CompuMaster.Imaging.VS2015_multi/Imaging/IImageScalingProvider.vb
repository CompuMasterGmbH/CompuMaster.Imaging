'Copyright 2004,2005,2010,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     The common interface for all image scaling classes
    ''' </summary>
    Public Interface IImageScalingProvider


        ''' <summary>
        '''     Resize an image to fit to the new maximum dimensions with a high quality, bilinear processing
        ''' </summary>
        ''' <param name="maximumWidth">The new maximum width</param>
        ''' <param name="maximumHeight">The new maximum height</param>
        ''' <remarks>
        '''     Input images which are smaller than the maximum dimensions won't generally get more large.
        '''     Widths or heights with zero value will be ignored for size calculation
        ''' </remarks>
        Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer)


        ''' <summary>
        '''     Resize an image to fit to the new maximum dimensions
        ''' </summary>
        ''' <param name="maximumWidth">The new maximum width</param>
        ''' <param name="maximumHeight">The new maximum height</param>
        ''' <param name="scaleMode">Fit or scale the image</param>
        ''' <remarks>
        '''     Input images which are smaller than the maximum dimensions won't generally get more large.
        '''     Widths or heights with zero value will be ignored for size calculation
        ''' </remarks>
        Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal scaleMode As ScaleMode)


        ''' <summary>
        '''     Image scaling modes
        ''' </summary>
        Enum ScaleMode

            ''' <summary>
            '''     The image will be resized with width and height values as maximum values
            ''' </summary>
            ''' <remarks>
            '''     Requires at least one value of width and height; zero values lead to ignoration when calculating new size
            '''     Width respectively height might change its value to reflect the new size information. See CurrentWidth and CurrentHeight for updated values
            ''' </remarks>
            Scale = 0

            ''' <summary>
            '''     The image will be resized to fill the complete area defined by width and height
            ''' </summary>
            ''' <remarks>
            '''     Requires width and height value, otherwise an exception will be thrown
            ''' </remarks>
            Fit = 1

            ''' <summary>
            '''     The image will be resized and centered to be completely inside of width and height, empty areas will be filled with the background color
            ''' </summary>
            ''' <remarks>
            '''     Requires width and height value, otherwise an exception will be thrown
            ''' </remarks>
            FitExact = 2
            ''' <summary>
            '''     The image will be resized with width and height values as maximum values, but only if the result is smaller.
            '''     That means a larger image will be resized to a smaller size, but a smaller image will not be enlarged.
            ''' </summary>
            ''' <remarks>
            '''     Requires at least one value of width and height; zero values lead to ignoration when calculating new size
            '''     Width respectively height might change its value to reflect the new size information. See CurrentWidth and CurrentHeight for updated values
            ''' </remarks>
            ScaleShrinkOnly = 3
        End Enum


        ''' <summary>
        '''     The resized image
        ''' </summary>
        ReadOnly Property ImageOutput() As System.Drawing.Image


        ''' <summary>
        '''     The input images where the resizing starts
        ''' </summary>
        Property ImageInput() As System.Drawing.Image


        ''' <summary>
        '''     The height of the output image
        ''' </summary>
        ReadOnly Property ResizedHeight() As Integer


        ''' <summary>
        '''     The width of the output image
        ''' </summary>
        ReadOnly Property ResizedWidth() As Integer


        ''' <summary>
        '''     Load an input file from disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Sub Load(ByVal filename As String)


        ''' <summary>
        '''     The default background color when fitting to width and height lead to empty areas
        ''' </summary>
        Property BackgroundColor() As System.Drawing.Color


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Sub Save(ByVal filename As String)


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        ''' <param name="format">The desired file format (e. g. JPG)</param>
        Sub Save(ByVal filename As String, ByVal format As System.Drawing.Imaging.ImageFormat)


        ''' <summary>
        '''     The image as JPG binary data, as a byte array to be stored e. g. on disc or database
        ''' </summary>
        Function ImageOutputData() As Byte()


        ''' <summary>
        '''     The image as binary data, as a byte array to be stored e. g. on disc or database
        ''' </summary>
        ''' <param name="format">The file format for the binary output</param>
        ''' <returns>The binary data of the image</returns>
        Function ImageOutputData(ByVal format As System.Drawing.Imaging.ImageFormat) As Byte()

    End Interface

End Namespace