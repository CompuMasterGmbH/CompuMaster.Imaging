'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    ''' 	Provices functionality for converting colour to black-and-white images.
    ''' </summary>
    Public Class BlackWhiteConverter
        Implements CompuMaster.Drawing.Imaging.IImageFilter

        ''' <summary>
        ''' 	Reference to the image to convert.
        ''' </summary>
        Private _Image As Image

        ''' <summary>
        ''' 	Gets or sets the image to convert / converted.
        ''' </summary>
        ''' <value>Sets the image</value>
        ''' <returns>Gets the current image, null if not yet set</returns>
        Public Property Image() As Image Implements IImageFilter.Image
            Get
                Return Me._Image
            End Get
            Set(ByVal Value As Image)
                Me._Image = Value
            End Set
        End Property

        ''' <summary>
        ''' 	The brightness threshold level to distinguish between black or white.
        ''' </summary>
        Private _ThresholdLevel As Integer = 128

        ''' <summary>
        ''' 	Gets or sets the brightness level used to distiguish between black
        ''' 	or white parts of the image (can be any value from 0 to 255).
        ''' </summary>
        ''' <value>Sets the current threshold level</value>
        ''' <returns>Gets the current threshold level</returns>
        Public Property ThresholdLevel() As Integer
            Get
                Return Me._ThresholdLevel
            End Get
            Set(ByVal Value As Integer)
                If ((Value >= 0) AndAlso (Value <= 255)) Then
                    Me._ThresholdLevel = Value
                Else
                    Me._ThresholdLevel = 0
                End If
            End Set
        End Property

        ''' <summary>
        ''' 	Converts the image to a black and white one.
        ''' </summary>
        Public Sub ApplyFilter() Implements IImageFilter.ApplyFilter
            Dim bitmap As Bitmap = DirectCast(Me._Image, Bitmap)
            Dim currentPixel As Color
            Dim currentPixelGreyValue As Integer
            For counterY As Integer = 0 To (_Image.Height - 1)
                For counterX As Integer = 0 To (_Image.Width - 1)
                    currentPixel = bitmap.GetPixel(counterX, counterY)
                    currentPixelGreyValue = CInt(Math.Round(CDbl((((currentPixel.R * 0.3) + (currentPixel.G * 0.59)) + (currentPixel.B * 0.11)))))
                    If (currentPixelGreyValue > Me._ThresholdLevel) Then
                        bitmap.SetPixel(counterX, counterY, Color.White)
                    Else
                        bitmap.SetPixel(counterX, counterY, Color.Black)
                    End If
                Next
            Next
        End Sub

    End Class

End Namespace
