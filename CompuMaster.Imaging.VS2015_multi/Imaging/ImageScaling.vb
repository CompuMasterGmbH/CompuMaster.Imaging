'Copyright 2004,2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     Image manimuplations
    ''' </summary>
    Public Class ImageScaling
        Inherits ImageScalingProviderBase
        Implements IDisposable


        ''' <summary>
        '''     Convert a byte array back to an image
        ''' </summary>
        ''' <param name="data">The array of bytes which represent the binary stream of JPG, GIF, PNG or other supported file formats</param>
        Public Sub Read(ByVal data As Byte())
            'ToDo: review code if it really works as promised in the description of this method and for which file formats
            Dim stream As New System.IO.MemoryStream(data)
            Try
                ImageInput = System.Drawing.Image.FromStream(stream)
                stream.Close()
            Finally
                stream.Dispose()
            End Try
        End Sub


        ''' <summary>
        '''     Convert a byte array back to an image
        ''' </summary>
        ''' <param name="data">The array of bytes which represent the binary stream of JPG, GIF, PNG or other supported file formats</param>
        ''' <param name="useEmbeddedColorManagement">Set to true if you want to use the color information embedded in the data stream</param>
        Public Sub Read(ByVal data As Byte(), ByVal useEmbeddedColorManagement As Boolean)
            'ToDo: review code if it really works as promised in the description of this method and for which file formats
            Dim stream As New System.IO.MemoryStream(data)
            Try
                ImageInput = System.Drawing.Image.FromStream(stream, useEmbeddedColorManagement)
                stream.Close()
            Finally
                stream.Dispose()
            End Try
        End Sub


        ''' <summary>
        '''     Convert a byte array back to an image
        ''' </summary>
        ''' <param name="data">The array of bytes which represent the binary stream of JPG, GIF, PNG or other supported file formats</param>
        ''' <param name="useEmbeddedColorManagement">Set to true if you want to use the color information embedded in the data stream</param>
        ''' <param name="validateImageData">Set to true to validate the image data</param>
        Public Sub Read(ByVal data As Byte(), ByVal useEmbeddedColorManagement As Boolean, ByVal validateImageData As Boolean)
            'ToDo: review code if it really works as promised in the description of this method and for which file formats
            Dim stream As New System.IO.MemoryStream(data)
            Try
                ImageInput = System.Drawing.Image.FromStream(stream, useEmbeddedColorManagement, validateImageData)
                stream.Close()
            Finally
                stream.Dispose()
            End Try

        End Sub


        ''' <summary>
        '''     Convert a data stream back to an image
        ''' </summary>
        ''' <param name="data">The binary stream of JPG, GIF, PNG or other supported file formats</param>
        ''' <param name="useEmbeddedColorManagement">Set to true if you want to use the color information embedded in the data stream</param>
        ''' <param name="validateImageData">Set to true to validate the image data</param>
        Public Sub Read(ByVal data As System.IO.Stream, ByVal useEmbeddedColorManagement As Boolean, ByVal validateImageData As Boolean)
            ImageInput = System.Drawing.Image.FromStream(data, useEmbeddedColorManagement, validateImageData)
        End Sub

        'ToDo: what is the range for JPG? --> add to documentation here
        'abaldauf: according to http://www.microsoft.com/germany/msdn/library/multimedia/gdiplus/KompressionVonJPEGDateienBeeinflussen.mspx?mfr=true
        '          this value can be between 0 and 100, where the compression (in %) is 100 - _Quality,
        '          so that a high _Quality results in a low compression
        'ToDo: what is a good value for web purposes as default?
        'abaldauf: from my experiences, you can see compression artifacts from 15% to 25% and higher
        '          (depends on the image's content). above this value, the winning of size is out of
        '          all proportion to the loss of quality (yet this may be just a personal view).
        Private _Quality As Integer = 85

        ''' <summary>
        '''     The image quality when saving files (e. g. important for JPG file format)
        ''' </summary>
        Public Property Quality() As Integer
            Get
                Return _Quality
            End Get
            Set(ByVal Value As Integer)
                _Quality = Value
            End Set
        End Property


        ''' <summary>
        '''     Load an input file from disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        ''' <param name="useEmbeddedColorManagement">Use color information already stored in the image file</param>
        Public Overloads Sub Load(ByVal filename As String, ByVal useEmbeddedColorManagement As Boolean)
            'Input validation
            If filename = Nothing Then
                Throw New ArgumentNullException("filename")
            ElseIf System.IO.File.Exists(filename) = False Then
                Throw New System.IO.FileNotFoundException("File not found", filename)
            End If

            'Processing
            Dim fs As System.IO.FileStream = Nothing
            Try
                fs = System.IO.File.OpenRead(filename)
                ImageInput = System.Drawing.Image.FromStream(fs, useEmbeddedColorManagement)
#If DEBUG Then
            Catch ex As Exception
                Throw New Exception("Error with reading file """ & filename & """", ex)
#End If
            Finally
                If Not fs Is Nothing Then
                    fs.Close()
                    fs.Dispose()
                End If
            End Try
            'ImageInput = System.Drawing.Image.FromFile(filename, useEmbeddedColorManagement)
            If ImageInput Is Nothing Then
                Throw New Exception("Reading from file failed")
            End If
        End Sub


        ''' <summary>
        '''     Load an input file from disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Public Overloads Overrides Sub Load(ByVal filename As String)
            ImageInput = System.Drawing.Image.FromFile(filename)
            If ImageInput Is Nothing Then
                Throw New Exception("Reading from file failed")
            End If
        End Sub


        ''' <summary>
        '''     Save the image as file on disc in the default file format (.png)
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        Public Overloads Overrides Sub Save(ByVal filename As String)
            ResizedImage.Save(filename)
        End Sub


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        ''' <param name="format">The desired file format (e. g. JPG)</param>
        Public Overloads Overrides Sub Save(ByVal filename As String, ByVal format As System.Drawing.Imaging.ImageFormat)
            ResizedImage.Save(filename, format)
        End Sub


        ''' <summary>
        '''     Save the image as file on disc
        ''' </summary>
        ''' <param name="filename">The file name on disc</param>
        ''' <param name="fileFormat">A file format as a codec information</param>
        Public Overloads Sub Save(ByVal filename As String, ByVal fileFormat As ImageCodecInfo)
            ' setup quality of image to save
            Dim Params As New EncoderParameters(1)
            Params.Param(0) = New EncoderParameter(Encoder.Quality, Quality)
            ResizedImage.Save(filename, fileFormat, Params)
        End Sub

        Private Graphic As Graphics


        ''' <summary>
        '''     Resize an image to fit to the new maximum dimensions
        ''' </summary>
        ''' <param name="maximumWidth">The new maximum width</param>
        ''' <param name="maximumHeight">The new maximum height</param>
        ''' <param name="resizeMethod">The method and quality of the resizing process</param>
        ''' <remarks>
        '''     Input images which are smaller than the maximum dimensions won't generally get more large.
        '''     Widths or heights with zero value will be ignored for size calculation
        ''' </remarks>
        Public Overloads Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal resizeMethod As System.Drawing.Drawing2D.InterpolationMode)
            Resize(maximumWidth, maximumHeight, IImageScalingProvider.ScaleMode.Scale, System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear)
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
        Public Overloads Overrides Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal scaleMode As IImageScalingProvider.ScaleMode)
            Resize(maximumWidth, maximumHeight, scaleMode, System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear)
        End Sub


        ''' <summary>
        '''     Resize an image
        ''' </summary>
        ''' <param name="maximumWidth">The new maximum width</param>
        ''' <param name="maximumHeight">The new maximum height</param>
        ''' <param name="resizeMethod">The method and quality of the resizing process</param>
        ''' <param name="scaleMode">Fit or scale the image</param>
        ''' <remarks>
        '''     Input images which are smaller than the maximum dimensions won't generally get more large.
        '''     Widths or heights with zero value will be ignored for size calculation
        ''' </remarks>
        Public Overloads Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal scaleMode As IImageScalingProvider.ScaleMode, ByVal resizeMethod As System.Drawing.Drawing2D.InterpolationMode)

            'Input parameter validation
            If maximumWidth <= 0 AndAlso maximumHeight <= 0 Then
                Throw New ArgumentException("At least one width or height value must be larger than zero", "maximumWidth, maximumHeight")
            ElseIf maximumWidth <= 0 Then
                maximumWidth = Integer.MaxValue
            ElseIf maximumHeight <= 0 Then
                maximumHeight = Integer.MaxValue
            End If

            'Input image validation
            If Me.ImageInput.Width = Nothing OrElse Me.ImageInput.Height = Nothing Then
                Throw New Exception("Input image with invalid, zero-sized dimensions")
            End If

            Dim NewWidth As Integer
            Dim NewHeight As Integer
            Dim scalingFactor As Double
            Dim offsetX As Integer = 0
            Dim offsetY As Integer = 0

            'Calculate new target dimensions
            Select Case scaleMode
                Case IImageScalingProvider.ScaleMode.Scale
                    If (maximumWidth / Me.ImageInput.Width) < (maximumHeight / Me.ImageInput.Height) Then
                        scalingFactor = maximumWidth / Me.ImageInput.Width
                    Else
                        scalingFactor = maximumHeight / Me.ImageInput.Height
                    End If
                    NewWidth = CType(Me.ImageInput.Width * scalingFactor, Integer)
                    NewHeight = CType(Me.ImageInput.Height * scalingFactor, Integer)

                Case IImageScalingProvider.ScaleMode.ScaleShrinkOnly
                    If (maximumWidth / Me.ImageInput.Width) < (maximumHeight / Me.ImageInput.Height) Then
                        scalingFactor = maximumWidth / Me.ImageInput.Width
                    Else
                        scalingFactor = maximumHeight / Me.ImageInput.Height
                    End If
                    If (scalingFactor < 1) Then
                        NewWidth = CType(Me.ImageInput.Width * scalingFactor, Integer)
                        NewHeight = CType(Me.ImageInput.Height * scalingFactor, Integer)
                    Else
                        NewWidth = Me.ImageInput.Width
                        NewHeight = Me.ImageInput.Height
                    End If

                Case IImageScalingProvider.ScaleMode.FitExact
                    If (maximumWidth / Me.ImageInput.Width) < (maximumHeight / Me.ImageInput.Height) Then
                        scalingFactor = maximumWidth / Me.ImageInput.Width
                    Else
                        scalingFactor = maximumHeight / Me.ImageInput.Height
                    End If
                    NewWidth = CType(Me.ImageInput.Width * scalingFactor, Integer)
                    NewHeight = CType(Me.ImageInput.Height * scalingFactor, Integer)
                    offsetX = CType((maximumWidth - NewWidth) / 2, Integer)
                    offsetY = CType((maximumHeight - NewHeight) / 2, Integer)

                Case IImageScalingProvider.ScaleMode.Fit
                    If (maximumWidth / Me.ImageInput.Width) > (maximumHeight / Me.ImageInput.Height) Then
                        scalingFactor = maximumWidth / Me.ImageInput.Width
                        offsetX = 0
                        offsetY = 0 - CInt((CInt(Me.ImageInput.Height * scalingFactor) - maximumHeight) / 2)
                    Else
                        scalingFactor = maximumHeight / Me.ImageInput.Height
                        offsetX = 0 - CInt(CInt(Me.ImageInput.Width * scalingFactor - maximumWidth) / 2)
                        offsetY = 0
                    End If
                    NewWidth = CType(Me.ImageInput.Width * scalingFactor, Integer)
                    NewHeight = CType(Me.ImageInput.Height * scalingFactor, Integer)

            End Select

            'resize image
            Try
                Select Case scaleMode
                    Case IImageScalingProvider.ScaleMode.FitExact
                        ResizedImage = New Bitmap(maximumWidth, maximumHeight, PixelFormat.Format24bppRgb)
                        Graphic = Graphics.FromImage(ResizedImage)
                        Graphic.InterpolationMode = resizeMethod
                        Graphic.FillRectangle(New SolidBrush(BackgroundColor), 0, 0, maximumWidth, maximumHeight)
                        Graphic.DrawImage(ImageInput, offsetX - 1, offsetY - 1, NewWidth + 2, NewHeight + 2)


                    Case IImageScalingProvider.ScaleMode.Fit
                        ResizedImage = New Bitmap(maximumWidth, maximumHeight, PixelFormat.Format24bppRgb)
                        Graphic = Graphics.FromImage(ResizedImage)
                        Graphic.InterpolationMode = resizeMethod
                        Graphic.FillRectangle(New SolidBrush(BackgroundColor), 0, 0, maximumWidth, maximumHeight)
                        Graphic.DrawImage(ImageInput, offsetX - 1, offsetY - 1, NewWidth + 2, NewHeight + 2)


                    Case IImageScalingProvider.ScaleMode.Scale
                        ResizedImage = New Bitmap(NewWidth, NewHeight, PixelFormat.Format24bppRgb)
                        Graphic = Graphics.FromImage(ResizedImage)
                        Graphic.InterpolationMode = resizeMethod
                        Graphic.FillRectangle(New SolidBrush(BackgroundColor), 0, 0, NewWidth, NewHeight)
                        Graphic.DrawImage(ImageInput, offsetX - 1, offsetY - 1, NewWidth + 2, NewHeight + 2)

                    Case IImageScalingProvider.ScaleMode.ScaleShrinkOnly
                        If ((NewWidth = ImageInput.Width) AndAlso (NewHeight = ImageInput.Height)) Then
                            ResizedImage = ImageInput
                        Else
                            ResizedImage = New Bitmap(NewWidth, NewHeight, PixelFormat.Format24bppRgb)
                            Graphic = Graphics.FromImage(ResizedImage)
                            Graphic.InterpolationMode = resizeMethod
                            Graphic.FillRectangle(New SolidBrush(BackgroundColor), 0, 0, NewWidth, NewHeight)
                            Graphic.DrawImage(ImageInput, offsetX - 1, offsetY - 1, NewWidth + 2, NewHeight + 2)
                        End If

                End Select
            Catch ex As Exception
                Throw New Exception("Resizing error; target dimensions are " & NewWidth & "x" & NewHeight, ex)
            End Try


        End Sub

#Region "IDisposable Support"
        ''' <summary>
        ''' Used to check for redundant calls
        ''' </summary>
        Private disposedValue As Boolean

        ''' <summary>
        ''' Dispose object
        ''' </summary>
        ''' <param name="disposing">Should be true if called by <see cref="Dispose"/>() and false if called by <code>Finalize()</code></param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then 'check if called multiple times
                If disposing Then 'Dispose managed code
                    Me.Graphic.Dispose()
                End If
                'Dispose unmanaged code
            End If
            disposedValue = True
        End Sub

        ''' <summary>
        ''' Dispose object
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
#End Region
    End Class

End Namespace