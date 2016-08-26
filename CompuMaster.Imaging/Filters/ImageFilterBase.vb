'Copyright 2005,2006,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    Public MustInherit Class ImageFilterBase
        Implements CompuMaster.Drawing.Imaging.IImageFilter

        Private _Measurement As Measurement = Measurement.Absolute
        Public Property Measurement() As Measurement
            Get
                Return _Measurement
            End Get
            Set(ByVal Value As Measurement)
                _Measurement = Value
            End Set
        End Property


        ''' <summary>
        '''     The color of a pixel of the main image
        ''' </summary>
        ''' <param name="x">x position</param>
        ''' <param name="y">y position</param>
        ''' <value>An integer calculated as following: R*65536+G*255+B</value>
        Protected Property ImagePixelColorValue(ByVal x As Integer, ByVal y As Integer) As Integer
            Get
                'ToDo: fix relative measurement
                'If Me.Measurement = Measurement.Relative Then ' prozentuale in absolute Dimensionen umrechnen
                '    x = CInt(x / 100 * Image.Width)
                '    Throw New Exception("test")
                '    y = CInt(y / 100 * Image.Height)
                'End If
                Dim mycolor As Color = CType(Image, Bitmap).GetPixel(x, y)
                Return ConvertRGBColorToIntegerColor(mycolor.R, mycolor.G, mycolor.B)
            End Get
            Set(ByVal Value As Integer)
                'ToDo: fix relative measurement
                'If Me.Measurement = Measurement.Relative Then ' prozentuale in absolute Dimensionen umrechnen
                '    x = CInt(x / 100 * Image.Width)
                '    y = CInt(y / 100 * Image.Height)
                'End If
                CType(Image, Bitmap).SetPixel(x, y, ConvertIntegerColorToDrawingColor(Value))
            End Set
        End Property

        ''' <summary>
        '''     The color of a pixel of the main image
        ''' </summary>
        ''' <param name="x">x position</param>
        ''' <param name="y">y position</param>
        ''' <value>An integer calculated as following: R*65536+G*255+B</value>
        Protected ReadOnly Property ImagePixelColor(ByVal x As Integer, ByVal y As Integer) As Color
            Get
                'ToDo: fix relative measurement
                'If Me.Measurement = Measurement.Relative Then ' prozentuale in absolute Dimensionen umrechnen
                '    x = CInt(x / 100 * Image.Width)
                '    y = CInt(y / 100 * Image.Height)
                '    Throw New Exception("test")
                'End If
                Return CType(Image, Bitmap).GetPixel(x, y)
            End Get
        End Property


        ''' <summary>
        '''     Converts the RGB colour components to a single 24Bit colour
        ''' </summary>
        ''' <param name="red">The colour's red saturation</param>
        ''' <param name="green">The colour's green saturation</param>
        ''' <param name="blue">The colour's blue saturation</param>
        ''' <returns></returns>
        Protected Function ConvertRGBColorToIntegerColor(ByVal red As Byte, ByVal green As Byte, ByVal blue As Byte) As Integer
            Return blue + green * 256 + red * 65536
        End Function


        ''' <summary>
        '''     Converts an integer colour value to a color object
        ''' </summary>
        ''' <param name="color"></param>
        ''' <returns></returns>
        Protected Function ConvertIntegerColorToDrawingColor(ByVal color As Integer) As Color
            Return System.Drawing.Color.FromArgb(color)
        End Function


        ''' <summary>
        '''     Converts a color object into an integer colour value
        ''' </summary>
        ''' <param name="color"></param>
        Protected Function ConvertDrawingColorToIntegerColor(ByVal color As Color) As Integer
            Return ConvertRGBColorToIntegerColor(color.R, color.G, color.B)
        End Function

        Private _Image As System.Drawing.Image

        ''' <summary>
        '''     The image which is subject of all manipulations
        ''' </summary>
        Public Property Image() As System.Drawing.Image Implements IImageFilter.Image
            Get
                Return _Image
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _Image = Value
            End Set
        End Property

        Public MustOverride Sub ApplyFilter() Implements IImageFilter.ApplyFilter

    End Class

End Namespace