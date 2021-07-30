'Copyright 2010,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    ''' 	This class provides functionality for rotating an image according to the
    ''' 	EXIF informations provided.
    ''' </summary>
    Public Class ExifRotator
        Implements CompuMaster.Drawing.Imaging.IImageFilter

        ''' <summary>
        ''' 	Keeps an internal reference to the ExifExtractor providing the EXIF informations.
        ''' </summary>
        Private _exifExtractor As ExifExtractor = Nothing

        ''' <summary>
        ''' 	Gets or sets the ExifExtractor to provide the EXIF informations.
        ''' </summary>
        ''' <value>
        ''' 	Reference to an instance of ExifExtractor
        ''' </value>
        ''' <returns>
        ''' 	Reference to an instance of ExifExtractor or null if not yet set
        ''' </returns>
        Public Property exifExtractor() As ExifExtractor
            Get
                Return _exifExtractor
            End Get
            Set(ByVal value As ExifExtractor)
                _exifExtractor = value
            End Set
        End Property

        ''' <summary>
        ''' 	Keeps an internal reference to the image to be rotated.
        ''' </summary>
        Private _image As Image = Nothing

        ''' <summary>
        ''' 	Gets or sets the image to be rotated.
        ''' </summary>
        ''' <value>
        ''' 	Reference to an instance of Image
        ''' </value>
        ''' <returns>
        ''' 	Reference to an instance of Image or null if not yet set
        ''' </returns>
        Public Property Image() As System.Drawing.Image Implements IImageFilter.Image
            Get
                Return _image
            End Get
            Set(ByVal value As Image)
                _image = value
            End Set
        End Property

        ''' <summary>
        ''' 	Applies the rotation if appropriate and possible.
        ''' </summary>
        Public Sub ApplyFilter() Implements IImageFilter.ApplyFilter
            If (_exifExtractor Is Nothing) OrElse (_image Is Nothing) Then
                Throw New Exception("ExifExtractor or Image is a null reference (which is not allowed).")
            End If
            If _exifExtractor("Orientation") IsNot Nothing Then
                Dim rotationType As RotateFlipType = orientationToFlipType(_exifExtractor("Orientation").ToString())
                If rotationType <> RotateFlipType.RotateNoneFlipNone Then
                    Dim imageRotator As New ImageRotator()
                    imageRotator.RotateFlipType = rotationType
                    imageRotator.Image = _image
                    imageRotator.ApplyFilter()
                End If
            End If
        End Sub

        ''' <summary>
        ''' 	Translates an EXIF Tag "Orientation" value to the according RotateFlipType
        ''' 	required to correct this.
        ''' </summary>
        ''' <param name="orientation">EXIF Tag "Orentation" value</param>
        ''' <returns>
        ''' 	The according RotateFlipType.
        ''' </returns>
        Private Shared Function orientationToFlipType(ByVal orientation As String) As RotateFlipType
            Select Case Integer.Parse(orientation)
                Case 1
                    Return RotateFlipType.RotateNoneFlipNone
                    Exit Select
                Case 2
                    Return RotateFlipType.RotateNoneFlipX
                    Exit Select
                Case 3
                    Return RotateFlipType.Rotate180FlipNone
                    Exit Select
                Case 4
                    Return RotateFlipType.Rotate180FlipX
                    Exit Select
                Case 5
                    Return RotateFlipType.Rotate90FlipX
                    Exit Select
                Case 6
                    Return RotateFlipType.Rotate90FlipNone
                    Exit Select
                Case 7
                    Return RotateFlipType.Rotate270FlipX
                    Exit Select
                Case 8
                    Return RotateFlipType.Rotate270FlipNone
                    Exit Select
                Case Else
                    Return RotateFlipType.RotateNoneFlipNone
            End Select
        End Function

    End Class

End Namespace
