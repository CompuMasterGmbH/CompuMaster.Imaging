'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     Rotate images by 0, 90, 180 or 270 degrees
    ''' </summary>
    Public Class ImageRotator
        Implements CompuMaster.Drawing.Imaging.IImageFilter


        ''' <summary>
        '''     Rotates the given image by the given value
        ''' </summary>
        Public Sub ApplyFilter() Implements IImageFilter.ApplyFilter
            Image.RotateFlip(Me.RotateFlipType)
        End Sub

        Private _Image As System.Drawing.Image

        ''' <summary>
        '''     Gets or sets the image
        ''' </summary>
        Public Property Image() As System.Drawing.Image Implements IImageFilter.Image
            Get
                Return _Image
            End Get
            Set(ByVal Value As System.Drawing.Image)
                _Image = Value
            End Set
        End Property

        Private _RotateFlipType As RotateFlipType

        ''' <summary>
        '''     Gets or sets the value by which the image will be rotated
        ''' </summary>
        Public Property RotateFlipType() As RotateFlipType
            Get
                Return _RotateFlipType
            End Get
            Set(ByVal Value As RotateFlipType)
                _RotateFlipType = Value
            End Set
        End Property

    End Class

End Namespace