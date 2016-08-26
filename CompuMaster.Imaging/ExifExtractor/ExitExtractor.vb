'Copyright 2010,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Text
Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.IO

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    ''' 	Class which provides functionality for extracting EXIF informations from images.
    ''' </summary>
    Public Class ExifExtractor
        Implements IEnumerable

        ''' <summary>
        ''' Get the individual property value by supplying property name
        ''' These are the valid property names :
        ''' 
        ''' "Exif IFD"
        ''' "Gps IFD"
        ''' "New Subfile Type"
        ''' "Subfile Type"
        ''' "Image Width"
        ''' "Image Height"
        ''' "Bits Per Sample"
        ''' "Compression"
        ''' "Photometric Interp"
        ''' "Thresh Holding"
        ''' "Cell Width"
        ''' "Cell Height"
        ''' "Fill Order"
        ''' "Document Name"
        ''' "Image Description"
        ''' "Equip Make"
        ''' "Equip Model"
        ''' "Strip Offsets"
        ''' "Orientation"
        ''' "Samples PerPixel"
        ''' "Rows Per Strip"
        ''' "Strip Bytes Count"
        ''' "Min Sample Value"
        ''' "Max Sample Value"
        ''' "X Resolution"
        ''' "Y Resolution"
        ''' "Planar Config"
        ''' "Page Name"
        ''' "X Position"
        ''' "Y Position"
        ''' "Free Offset"
        ''' "Free Byte Counts"
        ''' "Gray Response Unit"
        ''' "Gray Response Curve"
        ''' "T4 Option"
        ''' "T6 Option"
        ''' "Resolution Unit"
        ''' "Page Number"
        ''' "Transfer Funcition"
        ''' "Software Used"
        ''' "Date Time"
        ''' "Artist"
        ''' "Host Computer"
        ''' "Predictor"
        ''' "White Point"
        ''' "Primary Chromaticities"
        ''' "ColorMap"
        ''' "Halftone Hints"
        ''' "Tile Width"
        ''' "Tile Length"
        ''' "Tile Offset"
        ''' "Tile ByteCounts"
        ''' "InkSet"
        ''' "Ink Names"
        ''' "Number Of Inks"
        ''' "Dot Range"
        ''' "Target Printer"
        ''' "Extra Samples"
        ''' "Sample Format"
        ''' "S Min Sample Value"
        ''' "S Max Sample Value"
        ''' "Transfer Range"
        ''' "JPEG Proc"
        ''' "JPEG InterFormat"
        ''' "JPEG InterLength"
        ''' "JPEG RestartInterval"
        ''' "JPEG LosslessPredictors"
        ''' "JPEG PointTransforms"
        ''' "JPEG QTables"
        ''' "JPEG DCTables"
        ''' "JPEG ACTables"
        ''' "YCbCr Coefficients"
        ''' "YCbCr Subsampling"
        ''' "YCbCr Positioning"
        ''' "REF Black White"
        ''' "ICC Profile"
        ''' "Gamma"
        ''' "ICC Profile Descriptor"
        ''' "SRGB RenderingIntent"
        ''' "Image Title"
        ''' "Copyright"
        ''' "Resolution X Unit"
        ''' "Resolution Y Unit"
        ''' "Resolution X LengthUnit"
        ''' "Resolution Y LengthUnit"
        ''' "Print Flags"
        ''' "Print Flags Version"
        ''' "Print Flags Crop"
        ''' "Print Flags Bleed Width"
        ''' "Print Flags Bleed Width Scale"
        ''' "Halftone LPI"
        ''' "Halftone LPIUnit"
        ''' "Halftone Degree"
        ''' "Halftone Shape"
        ''' "Halftone Misc"
        ''' "Halftone Screen"
        ''' "JPEG Quality"
        ''' "Grid Size"
        ''' "Thumbnail Format"
        ''' "Thumbnail Width"
        ''' "Thumbnail Height"
        ''' "Thumbnail ColorDepth"
        ''' "Thumbnail Planes"
        ''' "Thumbnail RawBytes"
        ''' "Thumbnail Size"
        ''' "Thumbnail CompressedSize"
        ''' "Color Transfer Function"
        ''' "Thumbnail Data"
        ''' "Thumbnail ImageWidth"
        ''' "Thumbnail ImageHeight"
        ''' "Thumbnail BitsPerSample"
        ''' "Thumbnail Compression"
        ''' "Thumbnail PhotometricInterp"
        ''' "Thumbnail ImageDescription"
        ''' "Thumbnail EquipMake"
        ''' "Thumbnail EquipModel"
        ''' "Thumbnail StripOffsets"
        ''' "Thumbnail Orientation"
        ''' "Thumbnail SamplesPerPixel"
        ''' "Thumbnail RowsPerStrip"
        ''' "Thumbnail StripBytesCount"
        ''' "Thumbnail ResolutionX"
        ''' "Thumbnail ResolutionY"
        ''' "Thumbnail PlanarConfig"
        ''' "Thumbnail ResolutionUnit"
        ''' "Thumbnail TransferFunction"
        ''' "Thumbnail SoftwareUsed"
        ''' "Thumbnail DateTime"
        ''' "Thumbnail Artist"
        ''' "Thumbnail WhitePoint"
        ''' "Thumbnail PrimaryChromaticities"
        ''' "Thumbnail YCbCrCoefficients"
        ''' "Thumbnail YCbCrSubsampling"
        ''' "Thumbnail YCbCrPositioning"
        ''' "Thumbnail RefBlackWhite"
        ''' "Thumbnail CopyRight"
        ''' "Luminance Table"
        ''' "Chrominance Table"
        ''' "Frame Delay"
        ''' "Loop Count"
        ''' "Pixel Unit"
        ''' "Pixel PerUnit X"
        ''' "Pixel PerUnit Y"
        ''' "Palette Histogram"
        ''' "Exposure Time"
        ''' "F-Number"
        ''' "Exposure Prog"
        ''' "Spectral Sense"
        ''' "ISO Speed"
        ''' "OECF"
        ''' "Ver"
        ''' "DTOrig"
        ''' "DTDigitized"
        ''' "CompConfig"
        ''' "CompBPP"
        ''' "Shutter Speed"
        ''' "Aperture"
        ''' "Brightness"
        ''' "Exposure Bias"
        ''' "MaxAperture"
        ''' "SubjectDist"
        ''' "Metering Mode"
        ''' "LightSource"
        ''' "Flash"
        ''' "FocalLength"
        ''' "Maker Note"
        ''' "User Comment"
        ''' "DTSubsec"
        ''' "DTOrigSS"
        ''' "DTDigSS"
        ''' "FPXVer"
        ''' "ColorSpace"
        ''' "PixXDim"
        ''' "PixYDim"
        ''' "RelatedWav"
        ''' "Interop"
        ''' "FlashEnergy"
        ''' "SpatialFR"
        ''' "FocalXRes"
        ''' "FocalYRes"
        ''' "FocalResUnit"
        ''' "Subject Loc"
        ''' "Exposure Index"
        ''' "Sensing Method"
        ''' "FileSource"
        ''' "SceneType"
        ''' "CfaPattern"
        ''' "Gps Ver"
        ''' "Gps LatitudeRef"
        ''' "Gps Latitude"
        ''' "Gps LongitudeRef"
        ''' "Gps Longitude"
        ''' "Gps AltitudeRef"
        ''' "Gps Altitude"
        ''' "Gps GpsTime"
        ''' "Gps GpsSatellites"
        ''' "Gps GpsStatus"
        ''' "Gps GpsMeasureMode"
        ''' "Gps GpsDop"
        ''' "Gps SpeedRef"
        ''' "Gps Speed"
        ''' "Gps TrackRef"
        ''' "Gps Track"
        ''' "Gps ImgDirRef"
        ''' "Gps ImgDir"
        ''' "Gps MapDatum"
        ''' "Gps DestLatRef"
        ''' "Gps DestLat"
        ''' "Gps DestLongRef"
        ''' "Gps DestLong"
        ''' "Gps DestBearRef"
        ''' "Gps DestBear"
        ''' "Gps DestDistRef"
        ''' "Gps DestDist"
        ''' </summary>
        Default Public ReadOnly Property Item(ByVal index As String) As Object
            Get
                Return properties(index)
            End Get
        End Property

        ''' <summary>
        ''' 	Internal reference to the bitmap object from which the EXIF informations
        ''' 	are extracted.
        ''' </summary>
        Private bmp As System.Drawing.Bitmap

        ''' <summary>
        ''' 	Internal string which contains all extracted EXIF informations in a
        ''' 	textual form.
        ''' </summary>
        Private data As String

        ''' <summary>
        ''' 	Internal hash which translates between the numerical EXIF tag number and
        ''' 	the textual description of it.
        ''' </summary>
        Private myHash As ExifTagTranslation

        ''' <summary>
        ''' 	Internal hash which keeps all the EXIF tag-value pairs.
        ''' </summary>
        Private properties As Hashtable

        ''' <summary>
        ''' 	Returns the number of items stored.
        ''' </summary>
        ''' <returns>
        ''' 	The number of items stored
        ''' </returns>
        Friend ReadOnly Property Count() As Integer
            Get
                Return Me.properties.Count
            End Get
        End Property

        ''' <summary>
        ''' 	Sets the value of an EXIF tag.
        ''' </summary>
        ''' <param name="id">Identifier of the EXIF tag</param>
        ''' <param name="data">Value to be set</param>
        Public Sub setTag(ByVal id As Integer, ByVal data As String)
            Dim ascii As Encoding = Encoding.ASCII
            Me.setTag(id, data.Length, &H2, ascii.GetBytes(data))
        End Sub

        ''' <summary>
        ''' 	Sets the value of an EXIF tag.
        ''' </summary>
        ''' <param name="id">Identifier of the EXIF tag</param>
        ''' <param name="len">Length of data string</param>
        ''' <param name="type">Numeric code of the EXIF tag</param>
        ''' <param name="data">Value to be set</param>
        Public Sub setTag(ByVal id As Integer, ByVal len As Integer, ByVal type As Short, ByVal data As Byte())
            Dim p As PropertyItem = CreatePropertyItem(type, id, len, data)
            Me.bmp.SetPropertyItem(p)
            buildDB(Me.bmp.PropertyItems)
        End Sub

        ''' <summary>
        ''' 	Creates a property item representing a single EXIF tag.
        ''' </summary>
        ''' <param name="type">Numeric code of the EXIF tag</param>
        ''' <param name="tag">Identifier of the EXIF tag</param>
        ''' <param name="len">Length of data string</param>
        ''' <param name="value">Value of data string</param>
        ''' <returns>
        ''' 	The item which has been created
        ''' </returns>
        Private Shared Function CreatePropertyItem(ByVal type As Short, ByVal tag As Integer, ByVal len As Integer, ByVal value As Byte()) As PropertyItem
            Dim item As PropertyItem
            ' Loads a PropertyItem from a Jpeg image stored in the assembly as a resource.
            Dim assembly__1 As Assembly = Assembly.GetExecutingAssembly()
            Dim emptyBitmapStream As Stream = assembly__1.GetManifestResourceStream("ExifExtractor.decoy.jpg")
            Dim empty As System.Drawing.Image = System.Drawing.Image.FromStream(emptyBitmapStream)
            item = empty.PropertyItems(0)
            ' Copies the data to the property item.
            item.Type = type
            item.Len = len
            item.Id = tag
            item.Value = New Byte(value.Length - 1) {}
            value.CopyTo(item.Value, 0)
            Return item
        End Function

        ''' <summary>
        ''' 	Creates a new instance of ExifExtractor.
        ''' </summary>
        ''' <param name="bmp">Reference to a Bitmap from which the informations should be extracted</param>
        Public Sub New(ByRef bmp As System.Drawing.Bitmap)
            properties = New Hashtable()
            Me.bmp = bmp
            myHash = New ExifTagTranslation()
            buildDB(Me.bmp.PropertyItems)
        End Sub

        ''' <summary>
        ''' 	Creates a new instance of ExifExtractor.
        ''' </summary>
        ''' <param name="file">Path to the file from which the informations should be extracted</param>
        Public Sub New(ByVal file As String)
            properties = New Hashtable()
            myHash = New ExifTagTranslation()
            Me.buildDB(GetExifProperties(file))
        End Sub

        ''' <summary>
        ''' 	Loads the image property attributes from a file.
        ''' </summary>
        ''' <param name="fileName">Path to the file from which the property attributes should be extracted</param>
        ''' <returns>The extracted property attributes</returns>
        Public Shared Function GetExifProperties(ByVal fileName As String) As PropertyItem()
            Dim stream As New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(stream, True, False)
            Return image.PropertyItems
        End Function

        ''' <summary>
        ''' 	Extracts the EXIF informations from the image property attributes and stores them
        ''' 	in the current instance of this class.
        ''' </summary>
        ''' <param name="parr">The image property items</param>
        Private Sub buildDB(ByVal parr As System.Drawing.Imaging.PropertyItem())
            properties.Clear()
            data = ""
            Dim ascii As Encoding = Encoding.ASCII
            For Each p As System.Drawing.Imaging.PropertyItem In parr
                Dim v As String = ""
                Dim name As String = DirectCast(myHash(p.Id), String)
                ' tag not found. skip it
                If name Is Nothing Then
                    Continue For
                End If
                data += name & ": "
                '1 = BYTE An 8-bit unsigned integer.,
                If p.Type = &H1 Then
                    v = p.Value(0).ToString()
                    '2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
                ElseIf p.Type = &H2 Then
                    ' string					
                    v = ascii.GetString(p.Value)
                    '3 = SHORT A 16-bit (2 -byte) unsigned integer,
                ElseIf p.Type = &H3 Then
                    ' orientation // lookup table					
                    Select Case p.Id
                        Case &H8827
                            ' ISO
                            v = "ISO-" & convertToInt16U(p.Value).ToString()
                            Exit Select
                        Case &HA217
                            ' sensing method
                            If True Then
                                Select Case convertToInt16U(p.Value)
                                    Case 1
                                        v = "Not defined"
                                        Exit Select
                                    Case 2
                                        v = "One-chip color area sensor"
                                        Exit Select
                                    Case 3
                                        v = "Two-chip color area sensor"
                                        Exit Select
                                    Case 4
                                        v = "Three-chip color area sensor"
                                        Exit Select
                                    Case 5
                                        v = "Color sequential area sensor"
                                        Exit Select
                                    Case 7
                                        v = "Trilinear sensor"
                                        Exit Select
                                    Case 8
                                        v = "Color sequential linear sensor"
                                        Exit Select
                                    Case Else
                                        v = " reserved"
                                        Exit Select
                                End Select
                            End If
                            Exit Select
                        Case &H8822
                            ' aperture 
                            Select Case convertToInt16U(p.Value)
                                Case 0
                                    v = "Not defined"
                                    Exit Select
                                Case 1
                                    v = "Manual"
                                    Exit Select
                                Case 2
                                    v = "Normal program"
                                    Exit Select
                                Case 3
                                    v = "Aperture priority"
                                    Exit Select
                                Case 4
                                    v = "Shutter priority"
                                    Exit Select
                                Case 5
                                    v = "Creative program (biased toward depth of field)"
                                    Exit Select
                                Case 6
                                    v = "Action program (biased toward fast shutter speed)"
                                    Exit Select
                                Case 7
                                    v = "Portrait mode (for closeup photos with the background out of focus)"
                                    Exit Select
                                Case 8
                                    v = "Landscape mode (for landscape photos with the background in focus)"
                                    Exit Select
                                Case Else
                                    v = "reserved"
                                    Exit Select
                            End Select
                            Exit Select
                        Case &H9207
                            ' metering mode
                            Select Case convertToInt16U(p.Value)
                                Case 0
                                    v = "unknown"
                                    Exit Select
                                Case 1
                                    v = "Average"
                                    Exit Select
                                Case 2
                                    v = "CenterWeightedAverage"
                                    Exit Select
                                Case 3
                                    v = "Spot"
                                    Exit Select
                                Case 4
                                    v = "MultiSpot"
                                    Exit Select
                                Case 5
                                    v = "Pattern"
                                    Exit Select
                                Case 6
                                    v = "Partial"
                                    Exit Select
                                Case 255
                                    v = "Other"
                                    Exit Select
                                Case Else
                                    v = "reserved"
                                    Exit Select
                            End Select
                            Exit Select
                        Case &H9208
                            ' light source
                            If True Then
                                Select Case convertToInt16U(p.Value)
                                    Case 0
                                        v = "unknown"
                                        Exit Select
                                    Case 1
                                        v = "Daylight"
                                        Exit Select
                                    Case 2
                                        v = "Fluorescent"
                                        Exit Select
                                    Case 3
                                        v = "Tungsten"
                                        Exit Select
                                    Case 17
                                        v = "Standard light A"
                                        Exit Select
                                    Case 18
                                        v = "Standard light B"
                                        Exit Select
                                    Case 19
                                        v = "Standard light C"
                                        Exit Select
                                    Case 20
                                        v = "D55"
                                        Exit Select
                                    Case 21
                                        v = "D65"
                                        Exit Select
                                    Case 22
                                        v = "D75"
                                        Exit Select
                                    Case 255
                                        v = "other"
                                        Exit Select
                                    Case Else
                                        v = "reserved"
                                        Exit Select
                                End Select
                            End If
                            Exit Select
                        Case &H9209
                            If True Then
                                Select Case convertToInt16U(p.Value)
                                    Case 0
                                        v = "Flash did not fire"
                                        Exit Select
                                    Case 1
                                        v = "Flash fired"
                                        Exit Select
                                    Case 5
                                        v = "Strobe return light not detected"
                                        Exit Select
                                    Case 7
                                        v = "Strobe return light detected"
                                        Exit Select
                                    Case Else
                                        v = "reserved"
                                        Exit Select
                                End Select
                            End If
                            Exit Select
                        Case Else
                            v = convertToInt16U(p.Value).ToString()
                            Exit Select
                    End Select
                    '4 = LONG A 32-bit (4 -byte) unsigned integer,
                ElseIf p.Type = &H4 Then
                    ' orientation // lookup table					
                    v = convertToInt32U(p.Value).ToString()
                    '5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the//denominator.,
                ElseIf p.Type = &H5 Then
                    ' rational
                    Dim n As Byte() = New Byte(p.Len \ 2 - 1) {}
                    Dim d As Byte() = New Byte(p.Len \ 2 - 1) {}
                    Array.Copy(p.Value, 0, n, 0, p.Len \ 2)
                    Array.Copy(p.Value, p.Len \ 2, d, 0, p.Len \ 2)
                    Dim a As UInteger = convertToInt32U(n)
                    Dim b As UInteger = convertToInt32U(d)
                    Dim r As New Rational(a, b)
                    'convert here
                    Select Case p.Id
                        Case &H9202
                            ' aperture
                            v = "F/" & Math.Round(Math.Pow(Math.Sqrt(2), r.ToDouble()), 2).ToString()
                            Exit Select
                        Case &H920A
                            v = r.ToDouble().ToString()
                            Exit Select
                        Case &H829A
                            v = r.ToDouble().ToString()
                            Exit Select
                        Case &H829D
                            ' F-number
                            v = "F/" & r.ToDouble().ToString()
                            Exit Select
                        Case Else
                            v = r.ToString("/")
                            Exit Select
                    End Select
                    '7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
                ElseIf p.Type = &H7 Then
                    Select Case p.Id
                        Case &HA300
                            If True Then
                                If p.Value(0) = 3 Then
                                    v = "DSC"
                                Else
                                    v = "reserved"
                                End If
                                Exit Select
                            End If
                        Case &HA301
                            If p.Value(0) = 1 Then
                                v = "A directly photographed image"
                            Else
                                v = "Not a directly photographed image"
                            End If
                            Exit Select
                        Case Else
                            v = "-"
                            Exit Select
                    End Select
                    '9 = SLONG A 32-bit (4 -byte) signed integer (2's complement notation),
                ElseIf p.Type = &H9 Then
                    v = convertToInt32(p.Value).ToString()
                    '10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the
                    'denominator.
                ElseIf p.Type = &HA Then
                    ' rational
                    Dim n As Byte() = New Byte(p.Len \ 2 - 1) {}
                    Dim d As Byte() = New Byte(p.Len \ 2 - 1) {}
                    Array.Copy(p.Value, 0, n, 0, p.Len \ 2)
                    Array.Copy(p.Value, p.Len \ 2, d, 0, p.Len \ 2)
                    Dim a As Integer = convertToInt32(n)
                    Dim b As Integer = convertToInt32(d)
                    Dim r As New Rational(a, b)
                    ' convert here
                    Select Case p.Id
                        Case &H9201
                            ' shutter speed
                            v = "1/" & Math.Round(Math.Pow(2, r.ToDouble()), 2).ToString()
                            Exit Select
                        Case &H9203
                            v = Math.Round(r.ToDouble(), 4).ToString()
                            Exit Select
                        Case Else
                            v = r.ToString("/")
                            Exit Select
                    End Select
                End If
                ' add it to the list
                If properties(name) Is Nothing Then
                    properties.Add(name, v)
                End If
                ' cat it too
                data += v
                data += Environment.NewLine
            Next

        End Sub

        ''' <summary>
        ''' 	Returns a textual summary of all the EXIF informations stored in this instance.
        ''' </summary>
        ''' <returns>The string containing the informations.</returns>
        Public Overrides Function ToString() As String
            Return data
        End Function

        ''' <summary>
        ''' 	Internal conversion function.
        ''' </summary>
        ''' <param name="arr">Value to be converted</param>
        ''' <returns>The converted value</returns>
        Private Function convertToInt32(ByVal arr As Byte()) As Integer
            If arr.Length <> 4 Then
                Return 0
            Else
                Return arr(3) << 24 Or arr(2) << 16 Or arr(1) << 8 Or arr(0)
            End If
        End Function

        ''' <summary>
        ''' 	Internal conversion function.
        ''' </summary>
        ''' <param name="arr">Value to be converted</param>
        ''' <returns>The converted value</returns>
        Private Function convertToInt16(ByVal arr As Byte()) As Integer
            If arr.Length <> 2 Then
                Return 0
            Else
                Return arr(1) << 8 Or arr(0)
            End If
        End Function

        ''' <summary>
        ''' 	Internal conversion function.
        ''' </summary>
        ''' <param name="arr">Value to be converted</param>
        ''' <returns>The converted value</returns>
        Private Function convertToInt32U(ByVal arr As Byte()) As UInteger
            If arr.Length <> 4 Then
                Return 0
            Else
                Return Convert.ToUInt32(arr(3) << 24 Or arr(2) << 16 Or arr(1) << 8 Or arr(0))
            End If
        End Function

        ''' <summary>
        ''' 	Internal conversion function.
        ''' </summary>
        ''' <param name="arr">Value to be converted</param>
        ''' <returns>The converted value</returns>
        Private Function convertToInt16U(ByVal arr As Byte()) As UInteger
            If arr.Length <> 2 Then
                Return 0
            Else
                Return Convert.ToUInt16(arr(1) << 8 Or arr(0))
            End If
        End Function

        ''' <summary>
        ''' 	Function required by a class that implements IEnumerable.
        ''' </summary>
        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            ' TODO:  Add ExifExtractor.GetEnumerator implementation
            Return (New ExifExtractorEnumerator(Me.properties))
        End Function

    End Class

End Namespace