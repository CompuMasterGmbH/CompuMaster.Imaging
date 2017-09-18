'Copyright 2004,2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A base implementation of IImageScaling
    ''' </summary>
    Public MustInherit Class ImageScalingProviderBase
        Implements IImageScalingProvider


        ''' <summary>
        '''     The image as binary data, as a byte array to be stored e. g. on disc or database
        ''' </summary>
        ''' <returns>Binary data in JPEG file format</returns>
        Public Function ImageOutputData() As Byte() Implements IImageScalingProvider.ImageOutputData
            Return ImageOutputData(System.Drawing.Imaging.ImageFormat.Jpeg)
        End Function


        ''' <summary>
        '''     The image as binary data, as a byte array to be stored e. g. on disc or database
        ''' </summary>
        ''' <param name="format">The file format for the binary output</param>
        ''' <returns>The binary data of the image</returns>
        Function ImageOutputData(ByVal format As System.Drawing.Imaging.ImageFormat) As Byte() Implements IImageScalingProvider.ImageOutputData
            Dim s As New System.IO.MemoryStream
            Try
                Me.ImageOutput.Save(s, format)
                Dim Result As Byte()
                Result = s.ToArray
                s.Close()
                Return Result
            Finally
                s.Dispose()
            End Try
        End Function

        Private _BackgroundColor As System.Drawing.Color = Color.White 'Color.Transparent

        ''' <summary>
        '''     The default background color when fitting to width and height lead to empty areas
        ''' </summary>
        Public Property BackgroundColor() As System.Drawing.Color Implements IImageScalingProvider.BackgroundColor
            Get
                Return _BackgroundColor
            End Get
            Set(ByVal Value As System.Drawing.Color)
                _BackgroundColor = Value
            End Set
        End Property

        Protected ResizedImage As System.Drawing.Image

        ''' <summary>
        '''     The resized image
        ''' </summary>
        Public ReadOnly Property ImageOutput() As System.Drawing.Image Implements IImageScalingProvider.ImageOutput
            Get
                If ResizedImage Is Nothing Then
                    Return ImageInput
                Else
                    Return ResizedImage
                End If
            End Get
        End Property

        Private _ImageInput As System.Drawing.Image

        ''' <summary>
        '''     The input images where the resizing starts
        ''' </summary>
        Public Property ImageInput() As System.Drawing.Image Implements IImageScalingProvider.ImageInput
            Get
                Return _ImageInput
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _ImageInput = Value
            End Set
        End Property


        ''' <summary>
        '''     The height of the output image
        ''' </summary>
        Public ReadOnly Property ResizedHeight() As Integer Implements IImageScalingProvider.ResizedHeight
            Get
                Return ImageOutput.Height
            End Get
        End Property


        ''' <summary>
        '''     The width of the output image
        ''' </summary>
        Public ReadOnly Property ResizedWidth() As Integer Implements IImageScalingProvider.ResizedWidth
            Get
                Return ImageOutput.Width
            End Get
        End Property


        ''' <summary>
        '''     Resize an image to fit to the new maximum dimensions with a high quality, bilinear processing
        ''' </summary>
        ''' <param name="maximumWidth">The new maximum width</param>
        ''' <param name="maximumHeight">The new maximum height</param>
        ''' <remarks>
        '''     Input images which are smaller than the maximum dimensions won't generally get more large.
        '''     Widths or heights with zero value will be ignored for size calculation
        ''' </remarks>
        Public Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer) Implements IImageScalingProvider.Resize
            Resize(maximumWidth, maximumHeight, IImageScalingProvider.ScaleMode.Scale)
        End Sub


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
        Public MustOverride Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal scaleMode As IImageScalingProvider.ScaleMode) Implements IImageScalingProvider.Resize


        ''' <summary>
        '''     Load an input file from disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Public MustOverride Sub Load(ByVal filename As String) Implements IImageScalingProvider.Load


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Public MustOverride Overloads Sub Save(ByVal filename As String) Implements IImageScalingProvider.Save


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        ''' <param name="format">The desired file format (e. g. JPG)</param>
        Public MustOverride Overloads Sub Save(ByVal filename As String, ByVal format As System.Drawing.Imaging.ImageFormat) Implements IImageScalingProvider.Save

    End Class

End Namespace