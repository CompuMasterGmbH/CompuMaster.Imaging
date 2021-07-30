'Copyright 2015,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging

Namespace CompuMaster.Drawing.Imaging

    Public Class Watermark
        Implements IImageFilter

        Private _Image As System.Drawing.Image
        Public Property Image() As System.Drawing.Image Implements IImageFilter.Image
            Get
                Return _Image
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _Image = Value
            End Set
        End Property

        Private _WatermarkImage As System.Drawing.Image
        Public Property WatermarkImage() As System.Drawing.Image
            Get
                Return _WatermarkImage
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _WatermarkImage = Value
            End Set
        End Property

        Private _WatermarkPosition As ContentAlignment
        Public Property WatermarkPosition() As ContentAlignment
            Get
                Return _WatermarkPosition
            End Get
            Set(ByVal Value As ContentAlignment)
                _WatermarkPosition = Value
            End Set
        End Property

        Private _WatermarkTransparency As Integer = 0
        Public Property WatermarkTransparency() As Integer
            Get
                Return _WatermarkTransparency
            End Get
            Set(ByVal Value As Integer)
                If (Value >= 0) AndAlso (Value <= 100) Then
                    _WatermarkTransparency = Value
                Else
                    _WatermarkTransparency = 0
                End If
            End Set
        End Property



        Public Sub ApplyFilter() Implements IImageFilter.ApplyFilter
            Dim watermarkDrawPositionLeft As Integer = 0
            Dim watermarkDrawPositionTop As Integer = 0
            Dim watermarkSectionWidth As Integer = 0
            Dim watermarkSectionHeight As Integer = 0
            Dim watermarkSectionPositionLeft As Integer = 0
            Dim watermarkSectionPositionTop As Integer = 0
            ' calculate horizontal values
            If (_WatermarkPosition = ContentAlignment.TopLeft) OrElse (_WatermarkPosition = ContentAlignment.MiddleLeft) OrElse (_WatermarkPosition = ContentAlignment.BottomLeft) Then
                watermarkDrawPositionLeft = 0
                watermarkSectionPositionLeft = 0
                If (_WatermarkImage.Width <= _Image.Width) Then
                    watermarkSectionWidth = _WatermarkImage.Width
                Else
                    watermarkSectionWidth = _Image.Width
                End If
            End If
            If (_WatermarkPosition = ContentAlignment.TopCenter) OrElse (_WatermarkPosition = ContentAlignment.MiddleCenter) OrElse (_WatermarkPosition = ContentAlignment.BottomCenter) Then
                If (_WatermarkImage.Width <= _Image.Width) Then
                    watermarkDrawPositionLeft = (_Image.Width \ 2) - (_WatermarkImage.Width \ 2)
                    watermarkSectionPositionLeft = 0
                    watermarkSectionWidth = _WatermarkImage.Width
                Else
                    watermarkDrawPositionLeft = 0
                    watermarkSectionPositionLeft = (_WatermarkImage.Width \ 2) - (_Image.Width \ 2)
                    watermarkSectionWidth = _Image.Width
                End If
            End If
            If (_WatermarkPosition = ContentAlignment.TopRight) OrElse (_WatermarkPosition = ContentAlignment.MiddleRight) OrElse (_WatermarkPosition = ContentAlignment.BottomRight) Then
                If (_WatermarkImage.Width <= _Image.Width) Then
                    watermarkDrawPositionLeft = _Image.Width - _WatermarkImage.Width
                    watermarkSectionPositionLeft = 0
                    watermarkSectionWidth = _WatermarkImage.Width
                Else
                    watermarkDrawPositionLeft = 0
                    watermarkSectionPositionLeft = _WatermarkImage.Width - _Image.Width
                    watermarkSectionWidth = _Image.Width
                End If
            End If
            ' calculate vertical values
            If (_WatermarkPosition = ContentAlignment.TopLeft) OrElse (_WatermarkPosition = ContentAlignment.TopCenter) OrElse (_WatermarkPosition = ContentAlignment.TopRight) Then
                watermarkDrawPositionTop = 0
                watermarkSectionPositionTop = 0
                If (_WatermarkImage.Height <= _Image.Height) Then
                    watermarkSectionHeight = _WatermarkImage.Height
                Else
                    watermarkSectionHeight = _Image.Height
                End If
            End If
            If (_WatermarkPosition = ContentAlignment.MiddleLeft) OrElse (_WatermarkPosition = ContentAlignment.MiddleCenter) OrElse (_WatermarkPosition = ContentAlignment.MiddleRight) Then
                If (_WatermarkImage.Height <= _Image.Height) Then
                    watermarkDrawPositionTop = (_Image.Height \ 2) - (_WatermarkImage.Height \ 2)
                    watermarkSectionPositionTop = 0
                    watermarkSectionHeight = _WatermarkImage.Height
                Else
                    watermarkDrawPositionTop = 0
                    watermarkSectionPositionTop = (_WatermarkImage.Height \ 2) - (_Image.Height \ 2)
                    watermarkSectionHeight = _Image.Height
                End If
            End If
            If (_WatermarkPosition = ContentAlignment.BottomLeft) OrElse (_WatermarkPosition = ContentAlignment.BottomCenter) OrElse (_WatermarkPosition = ContentAlignment.BottomRight) Then
                If (_WatermarkImage.Height <= _Image.Height) Then
                    watermarkDrawPositionTop = _Image.Height - _WatermarkImage.Height
                    watermarkSectionPositionTop = 0
                    watermarkSectionHeight = _WatermarkImage.Height
                Else
                    watermarkDrawPositionTop = 0
                    watermarkSectionPositionTop = _WatermarkImage.Height - _Image.Height
                    watermarkSectionHeight = _Image.Height
                End If
            End If
            drawWatermarkOnImage(_WatermarkImage, watermarkDrawPositionLeft, watermarkDrawPositionTop, watermarkSectionPositionLeft, watermarkSectionPositionTop, watermarkSectionWidth, watermarkSectionHeight)
        End Sub

        Private Sub drawWatermarkOnImage(ByRef watermarkImage As Image, ByVal watermarkDrawPositionX As Integer, ByVal watermarkDrawPositionY As Integer, ByVal watermarkSectionX As Integer, ByVal watermarkSectionY As Integer, ByVal watermarkSectionWidth As Integer, ByVal watermarkSectionHeight As Integer)
            Dim imageAttributes As New imageAttributes
            Dim colorMatrix As New colorMatrix(New Single()() {New Single() {1, 0, 0, 0, 0}, New Single() {0, 1, 0, 0, 0}, New Single() {0, 0, 1, 0, 0}, New Single() {0, 0, 0, CSng((100 - _WatermarkTransparency) / 100), 0}, New Single() {0, 0, 0, 0, 1}})
            imageAttributes.SetColorMatrix(colorMatrix)
            Dim targetRect As Rectangle = New Rectangle(watermarkDrawPositionX, watermarkDrawPositionY, watermarkSectionWidth, watermarkSectionHeight)
            Dim imageGraphics As Graphics = Graphics.FromImage(_Image)
            imageGraphics.DrawImage(watermarkImage, targetRect, watermarkSectionX, watermarkSectionY, watermarkSectionWidth, watermarkSectionHeight, GraphicsUnit.Pixel, imageAttributes)
            imageGraphics.Dispose()
            imageAttributes.Dispose()
        End Sub

    End Class

End Namespace