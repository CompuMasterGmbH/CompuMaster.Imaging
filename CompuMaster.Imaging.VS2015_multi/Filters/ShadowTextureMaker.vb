'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A class which includes all the functionality for applying different
    '''     shadow effects to an image using a textured background instead of a
    '''     fixed colour background
    ''' </summary>
    Public Class ShadowTextureMaker
        Inherits ShadowMaker


        ''' <summary>
        '''     Draws the background image's texture to the area around the main image
        '''     If necessary, the background texture will be patterned, cut out or
        '''     resized to cover the whole area
        ''' </summary>
        Protected Sub ApplyOuterFrameTexture()
            ' Außenrand (Backgroundtextur)
            For Counter1 As Integer = 0 To Image.Height - 1
                For Counter2 As Integer = 0 To FrameWidth - 1  ' linker Rand
                    ImagePixelColorValue(Counter2, Counter1) = TexturePixel(Counter2 Mod TextureImage.Width, Counter1 Mod TextureImage.Height)
                Next
                For Counter2 As Integer = 0 To FrameWidth + ShadowWidth + ShadowGradientWidth - 1  ' rechter Rand
                    ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth), Counter1) = TexturePixel((Counter2 + Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth)) Mod TextureImage.Width, Counter1 Mod TextureImage.Height)
                Next
            Next
            For Counter1 As Integer = 0 To Image.Width - 1
                For Counter2 As Integer = 0 To FrameWidth - 1  ' oberer Rand
                    ImagePixelColorValue(Counter1, Counter2) = TexturePixel(Counter1 Mod TextureImage.Width, Counter2 Mod TextureImage.Height)
                Next
                For Counter2 As Integer = 0 To FrameWidth + ShadowWidth + ShadowGradientWidth - 1   ' unterer Rand
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth)) = TexturePixel(Counter1 Mod TextureImage.Width, (Counter2 + Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth)) Mod TextureImage.Height)
                Next
            Next
        End Sub


        ''' <summary>
        '''     Applies the shadow effects to the image according to the given parameters
        ''' </summary>
        Public Overrides Sub ApplyFilter()
            If (((FrameWidth + ShadowWidth + ShadowGradientWidth) > Image.Width) Or ((FrameWidth + ShadowWidth + ShadowGradientWidth) > Image.Height) Or (ShadowStrength < 1) Or (ShadowStrength > 100)) Then
                Throw New Exception("Error in Method ApplyFilter(): invalid parameters; image too small for this filter settings?")
            Else
                ApplyOuterFrameTexture()
                ApplyShadow()
                ApplyShadowGradient()
            End If
        End Sub

        Private _TextureImage As System.Drawing.Image
        Private _ResizedTextureImage As System.Drawing.Image

        ''' <summary>
        '''     Gets or sets the image that will be used as background texture
        ''' </summary>
        Public Property TextureImage() As System.Drawing.Image
            Get
                If _ResizedTextureImage Is Nothing Then
                    If _TextureImage Is Nothing Then
                        'Load the texture, now
                        _TextureImage = Image.FromFile(Me.TextureImagePath)
                    End If
                    Dim sizer As New CompuMaster.Drawing.Imaging.ImageScaling
                    sizer.ImageInput = _TextureImage
                    sizer.Resize(Image.Width, Image.Height, CompuMaster.Drawing.Imaging.IImageScalingProvider.ScaleMode.Fit)
                    _ResizedTextureImage = sizer.ImageOutput 'CType(sizer.ImageOutput.Clone, System.Drawing.Image)
                End If
                Return _ResizedTextureImage
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _TextureImage = Value
                _ResizedTextureImage = Nothing
            End Set
        End Property

        Private _TextureImagePath As String

        ''' <summary>
        '''     The path to the background texture image
        ''' </summary>
        Public Property TextureImagePath() As String
            Get
                Return _TextureImagePath
            End Get
            Set(ByVal Value As String)
                _TextureImagePath = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets a pixel's 24bit integer colour value at the given positon
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        Private Property TexturePixel(ByVal x As Integer, ByVal y As Integer) As Integer
            Get
                'ToDo: fix relative measurement
                'Dim orgx As Integer = x, orgy As Integer = y
                'If Me.Measurement = Measurement.Relative Then ' prozentuale in absolute Dimensionen umrechnen
                '    x = CInt(x / 100 * TextureImage.Width)
                '    y = CInt(y / 100 * TextureImage.Height)
                'End If
                'Try
                '    Dim dfd As Color = CType(TextureImage, Bitmap).GetPixel(x, y)
                'Catch
                '    Throw New Exception("x=" & x & " / y=" & y & " / " & "orgx=" & orgx & " / orgy=" & orgy & " / " & TextureImage.Width & " / " & TextureImage.Height)
                'End Try
                Return MyBase.ConvertDrawingColorToIntegerColor(CType(TextureImage, Bitmap).GetPixel(x, y))
            End Get
            Set(ByVal Value As Integer)
                'ToDo: fix relative measurement
                'If Me.Measurement = Measurement.Relative Then ' prozentuale in absolute Dimensionen umrechnen
                '    x = CInt(x / 100 * TextureImage.Width)
                '    Throw New Exception("kjökj")
                '    y = CInt(y / 100 * TextureImage.Height)
                'End If
                Dim mycolor As Color = MyBase.ConvertIntegerColorToDrawingColor(Value)
                CType(TextureImage, Bitmap).SetPixel(x, y, mycolor)
            End Set
        End Property


        ''' <summary>
        '''     Initializes a new instance of this class and sets the default values
        ''' </summary>
        Public Sub New()
            MyBase.New()

            Me.BackgroundBlue = 90
            Me.BackgroundGreen = 90
            Me.BackgroundRed = 90

            Me.ShadowBlue = 10
            Me.ShadowGreen = 10
            Me.ShadowRed = 10

            Me.FrameWidth = 10

            Me.ShadowGradientWidth = 5

            Me.Measurement = Measurement.Absolute

            Me.ShadowStrength = 50

        End Sub

    End Class

End Namespace