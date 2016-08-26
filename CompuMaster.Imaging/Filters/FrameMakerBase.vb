'Copyright 2005,2006,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Math

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A class which includes all the functionality for applying different
    '''     frame effects to an image.
    ''' </summary>
    Public MustInherit Class FrameMakerBase
        Inherits ImageFilterBase

        ''' <summary>
        '''     Applies the selected frame effect to the given image
        ''' </summary>
        Public Overrides Sub ApplyFilter()
            If (((_OuterFrameWidthLeft + _OuterFrameWidthRight + _MiddleFrameWidthLeft + _MiddleFrameWidthRight + _InnerFrameWidthLeft + _InnerFrameWidthRight) > Image.Width) _
            Or ((_OuterFrameWidthUpper + _OuterFrameWidthLower + _MiddleFrameWidthUpper + _MiddleFrameWidthLower + _InnerFrameWidthUpper + _InnerFrameWidthLower) > Image.Height) _
            Or (_InnerFrameWidthLeft = 1) Or (_InnerFrameWidthRight = 1) Or (_InnerFrameWidthUpper = 1) Or (_InnerFrameWidthLower = 1) _
            Or (_InnerFrameGradientCoverageStartValue < 0) Or (_InnerFrameGradientCoverageStartValue >= 1) Or (_InnerFrameGradientCoverageEndValue <= 0) Or (_InnerFrameGradientCoverageEndValue > 1) _
            Or (_InnerFrameGradientCoverageStartValue > _InnerFrameGradientCoverageEndValue) Or (_InnerFrameGradientFadeModifier <= 0)) Then
                Throw New Exception("Error in Method ApplyFilter(): invalid parameters; image too small for this filter settings?")
            Else
                ' Außenrahmen (solide)
                ApplyOuterFrame()
                ' Mittenrahmen (solide)
                ApplyMiddleFrame()
                ' Innenrahmen (Verlauf)
                If (_InnerFrameGradientCoverageStartValue > 0) Then
                    ' Zunächst festen Transparenzeffekt (mit dem Starttransparenzwert) zeichnen
                    ApplyInnerFrame(_InnerFrameGradientCoverageStartValue, _InnerFrameGradientCoverageStartValue, False)
                End If
                If (_InnerFrameGradientCoverageEndValue > _InnerFrameGradientCoverageStartValue) Then
                    ' Eventuell den Verlauf (mit Starttransparenz 0, da sonst falscher Effekt) zeichnen
                    ApplyInnerFrame(0, _InnerFrameGradientCoverageEndValue, True)
                End If
            End If
        End Sub


        ''' <summary>
        '''     Draws the outer frame onto the image
        ''' </summary>
        Protected Sub ApplyOuterFrame()
            Dim OuterFrameFarbe As Integer
            ' Außenrahmen zeichnen (solide)
            Try
                OuterFrameFarbe = OuterFrameRed + (OuterFrameGreen * CType(256, Integer)) + (OuterFrameBlue * CType(65536, Integer))
            Catch ex As Exception
                Throw New Exception("OuterFrameRed=" & OuterFrameRed & " / OuterFrameGreen=" & OuterFrameGreen & " / OuterFrameBlue=" & OuterFrameBlue, ex)
            End Try
            For Counter1 As Integer = 0 To Image.Height - 1     ' linker Rand
                For Counter2 As Integer = 0 To _OuterFrameWidthLeft - 1
                    ImagePixelColorValue(Counter2, Counter1) = OuterFrameFarbe
                Next
            Next
            For Counter1 As Integer = 0 To Image.Height - 1     ' rechter Rand
                For Counter2 As Integer = 0 To _OuterFrameWidthRight - 1
                    ImagePixelColorValue(Counter2 + Image.Width - _OuterFrameWidthRight, Counter1) = OuterFrameFarbe
                Next
            Next
            For Counter1 As Integer = 0 To Image.Width - 1      ' oberer Rand
                For Counter2 As Integer = 0 To _OuterFrameWidthUpper - 1
                    ImagePixelColorValue(Counter1, Counter2) = OuterFrameFarbe
                Next
            Next
            For Counter1 As Integer = 0 To Image.Width - 1      ' unterer Rand
                For Counter2 As Integer = 0 To _OuterFrameWidthLower - 1
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - _OuterFrameWidthLower) = OuterFrameFarbe
                Next
            Next
        End Sub


        ''' <summary>
        '''     Draws the middle frame onto the image
        ''' </summary>
        Protected Sub ApplyMiddleFrame()
            Dim Startval As Integer
            Dim Endval As Integer
            Dim MiddleFrameFarbe As Integer
            ' Mittenrahmen zeichnen (solide)
            MiddleFrameFarbe = MiddleFrameRed + (MiddleFrameGreen * CType(256, Integer)) + (MiddleFrameBlue * CType(65536, Integer))
            Startval = _OuterFrameWidthUpper                        ' äußeren Rahmen nicht übermalen (oben)
            Endval = Image.Height - (_OuterFrameWidthLower + 1)     ' äußeren Rahmen nicht übermalen (unten)
            If ((FrameType = 2) Or (FrameType = 4)) Then            ' außer wenn der Rahmeneffekt dies vorsieht
                Startval = 0
                Endval = Image.Height - 1
            End If
            For Counter1 As Integer = Startval To Endval   ' linker Rand
                For Counter2 As Integer = 0 To _MiddleFrameWidthLeft - 1
                    ImagePixelColorValue(Counter2 + _OuterFrameWidthLeft, Counter1) = MiddleFrameFarbe
                Next
            Next
            For Counter1 As Integer = Startval To Endval   ' rechter Rand
                For Counter2 As Integer = 0 To _MiddleFrameWidthRight - 1
                    ImagePixelColorValue(Counter2 + Image.Width - (_OuterFrameWidthRight + _MiddleFrameWidthRight), Counter1) = MiddleFrameFarbe
                Next
            Next
            Startval = _OuterFrameWidthLeft                         ' äußeren Rahmen nicht übermalen (links)
            Endval = Image.Width - (_OuterFrameWidthRight + 1)      ' äußeren Rahmen nicht übermalen (rechts)
            If ((FrameType = 2) Or (FrameType = 4)) Then            ' außer wenn der Rahmeneffekt dies vorsieht
                Startval = 0
                Endval = Image.Width - 1
            End If
            For Counter1 As Integer = Startval To Endval   ' oberer Rand
                For Counter2 As Integer = 0 To _MiddleFrameWidthUpper - 1
                    ImagePixelColorValue(Counter1, Counter2 + _OuterFrameWidthUpper) = MiddleFrameFarbe
                Next
            Next
            For Counter1 As Integer = Startval To Endval   ' unterer Rand
                For Counter2 As Integer = 0 To _MiddleFrameWidthLower - 1
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (_OuterFrameWidthLower + _MiddleFrameWidthLower)) = MiddleFrameFarbe
                Next
            Next
        End Sub


        ''' <summary>
        '''     Draws the inner frame (effect) onto the image
        ''' </summary>
        ''' <param name="GradientCoverageStartValue"></param>
        ''' <param name="GradientCoverageEndValue"></param>
        Protected Sub ApplyInnerFrame(ByVal GradientCoverageStartValue As Double, ByVal GradientCoverageEndValue As Double, ByVal DoubleDrawCorners As Boolean)
            Dim Startval As Integer
            Dim Endval As Integer
            Dim tempcol As Integer
            Dim Blueval As Byte
            Dim Greenval As Byte
            Dim Redval As Byte
            Dim tempfact As Double
            ' Innenrahmen zeichnen (Farbverlauf oder festen Transparenzeffekt)
            Startval = _OuterFrameWidthUpper + _MiddleFrameWidthUpper   ' äußeren & mittleren Rahmen nicht übermalen (oben)
            Endval = Image.Height - (_OuterFrameWidthLower + _MiddleFrameWidthLower + 1)    ' (und unten)
            If ((FrameType = 3) Or (FrameType = 4)) Then                ' außer wenn der Rahmeneffekt dies vorsieht
                Startval = 0
                Endval = Image.Height - 1
            End If
            For Counter1 As Integer = Startval To Endval    ' linker Rand
                For Counter2 As Integer = 0 To _InnerFrameWidthLeft - 1
                    If (DoubleDrawCorners OrElse (Counter1 < (_OuterFrameWidthUpper + _MiddleFrameWidthUpper)) OrElse
                    ((Counter1 >= (_OuterFrameWidthUpper + _MiddleFrameWidthUpper + _InnerFrameWidthUpper)) AndAlso (Counter1 < (Image.Height - (_InnerFrameWidthLower + _MiddleFrameWidthLower + _OuterFrameWidthLower)))) OrElse
                    ((Counter1 >= (Image.Height - (_MiddleFrameWidthLower + _OuterFrameWidthLower))))) Then
                        tempfact = Math.Pow(((_InnerFrameWidthLeft - 1) - Counter2) / (_InnerFrameWidthLeft - 1), _InnerFrameGradientFadeModifier)
                        tempfact = GradientCoverageStartValue + ((GradientCoverageEndValue - GradientCoverageStartValue) * tempfact)
                        tempcol = ImagePixelColorValue(Counter2 + _OuterFrameWidthLeft + _MiddleFrameWidthLeft, Counter1)
                        Redval = CByte(tempcol Mod 256)
                        tempcol = CInt((tempcol - Redval) / 256)
                        Greenval = CByte(tempcol Mod 256)
                        tempcol = CInt((tempcol - Greenval) / 256)
                        Blueval = CByte(tempcol Mod 256)
                        If (InnerFrameRed > Redval) Then
                            Redval = CByte(Redval + Round((InnerFrameRed - Redval) * tempfact))
                        Else
                            Redval = CByte(Redval - Round((Redval - InnerFrameRed) * tempfact))
                        End If
                        If (InnerFrameGreen > Greenval) Then
                            Greenval = CByte(Greenval + Round((InnerFrameGreen - Greenval) * tempfact))
                        Else
                            Greenval = CByte(Greenval - Round((Greenval - InnerFrameGreen) * tempfact))
                        End If
                        If (InnerFrameBlue > Blueval) Then
                            Blueval = CByte(Blueval + Round((InnerFrameBlue - Blueval) * tempfact))
                        Else
                            Blueval = CByte(Blueval - Round((Blueval - InnerFrameBlue) * tempfact))
                        End If
                        ImagePixelColorValue(Counter2 + _OuterFrameWidthLeft + _MiddleFrameWidthLeft, Counter1) = Redval + (Greenval * (256)) + (Blueval * (65536))
                    End If
                Next
            Next
            For Counter1 As Integer = Startval To Endval    ' rechter Rand
                For Counter2 As Integer = 0 To _InnerFrameWidthRight - 1
                    If (DoubleDrawCorners OrElse (Counter1 < (_OuterFrameWidthUpper + _MiddleFrameWidthUpper)) OrElse
                    ((Counter1 >= (_OuterFrameWidthUpper + _MiddleFrameWidthUpper + _InnerFrameWidthUpper)) AndAlso (Counter1 < (Image.Height - (_InnerFrameWidthLower + _MiddleFrameWidthLower + _OuterFrameWidthLower)))) OrElse
                    ((Counter1 >= (Image.Height - (_MiddleFrameWidthLower + _OuterFrameWidthLower))))) Then
                        tempfact = Math.Pow(Counter2 / (_InnerFrameWidthRight - 1), _InnerFrameGradientFadeModifier)
                        tempfact = GradientCoverageStartValue + ((GradientCoverageEndValue - GradientCoverageStartValue) * tempfact)
                        tempcol = ImagePixelColorValue(Counter2 + Image.Width - (_InnerFrameWidthRight + _MiddleFrameWidthRight + _OuterFrameWidthRight), Counter1)
                        Redval = CByte(tempcol Mod 256)
                        tempcol = CInt((tempcol - Redval) / 256)
                        Greenval = CByte(tempcol Mod 256)
                        tempcol = CInt((tempcol - Greenval) / 256)
                        Blueval = CByte(tempcol Mod 256)
                        If (InnerFrameRed > Redval) Then
                            Redval = CByte(Redval + Round((InnerFrameRed - Redval) * tempfact))
                        Else
                            Redval = CByte(Redval - Round((Redval - InnerFrameRed) * tempfact))
                        End If
                        If (InnerFrameGreen > Greenval) Then
                            Greenval = CByte(Greenval + Round((InnerFrameGreen - Greenval) * tempfact))
                        Else
                            Greenval = CByte(Greenval - Round((Greenval - InnerFrameGreen) * tempfact))
                        End If
                        If (InnerFrameBlue > Blueval) Then
                            Blueval = CByte(Blueval + Round((InnerFrameBlue - Blueval) * tempfact))
                        Else
                            Blueval = CByte(Blueval - Round((Blueval - InnerFrameBlue) * tempfact))
                        End If
                        ImagePixelColorValue(Counter2 + Image.Width - (_InnerFrameWidthRight + _MiddleFrameWidthRight + _OuterFrameWidthRight), Counter1) = Redval + (Greenval * (256)) + (Blueval * (65536))
                    End If
                Next
            Next
            Startval = _OuterFrameWidthLeft + _MiddleFrameWidthLeft     ' äußeren & mittleren Rahmen nicht übermalen (links)
            Endval = Image.Width - (_OuterFrameWidthRight + _MiddleFrameWidthRight + 1)     ' (und rechts)
            If ((FrameType = 3) Or (FrameType = 4)) Then                ' außer wenn der Rahmeneffekt dies vorsieht
                Startval = 0
                Endval = Image.Width - 1
            End If
            For Counter1 As Integer = Startval To Endval    ' oberer Rand
                For Counter2 As Integer = 0 To _InnerFrameWidthUpper - 1
                    tempfact = Math.Pow(((_InnerFrameWidthUpper - 1) - Counter2) / (_InnerFrameWidthUpper - 1), _InnerFrameGradientFadeModifier)
                    tempfact = GradientCoverageStartValue + ((GradientCoverageEndValue - GradientCoverageStartValue) * tempfact)
                    tempcol = ImagePixelColorValue(Counter1, Counter2 + _OuterFrameWidthUpper + _MiddleFrameWidthUpper)
                    Redval = CByte(tempcol Mod 256)
                    tempcol = CInt((tempcol - Redval) / 256)
                    Greenval = CByte(tempcol Mod 256)
                    tempcol = CInt((tempcol - Greenval) / 256)
                    Blueval = CByte(tempcol Mod 256)
                    If (InnerFrameRed > Redval) Then
                        Redval = CByte(Redval + Round((InnerFrameRed - Redval) * tempfact))
                    Else
                        Redval = CByte(Redval - Round((Redval - InnerFrameRed) * tempfact))
                    End If
                    If (InnerFrameGreen > Greenval) Then
                        Greenval = CByte(Greenval + Round((InnerFrameGreen - Greenval) * tempfact))
                    Else
                        Greenval = CByte(Greenval - Round((Greenval - InnerFrameGreen) * tempfact))
                    End If
                    If (InnerFrameBlue > Blueval) Then
                        Blueval = CByte(Blueval + Round((InnerFrameBlue - Blueval) * tempfact))
                    Else
                        Blueval = CByte(Blueval - Round((Blueval - InnerFrameBlue) * tempfact))
                    End If
                    ImagePixelColorValue(Counter1, Counter2 + _OuterFrameWidthUpper + _MiddleFrameWidthUpper) = Redval + (Greenval * (256)) + (Blueval * (65536))
                Next
            Next
            For Counter1 As Integer = Startval To Endval    ' unterer Rand
                For Counter2 As Integer = 0 To _InnerFrameWidthLower - 1
                    tempfact = Math.Pow(Counter2 / (_InnerFrameWidthLower - 1), _InnerFrameGradientFadeModifier)
                    tempfact = GradientCoverageStartValue + ((GradientCoverageEndValue - GradientCoverageStartValue) * tempfact)
                    tempcol = ImagePixelColorValue(Counter1, Counter2 + Image.Height - (_InnerFrameWidthLower + _MiddleFrameWidthLower + _OuterFrameWidthLower))
                    Redval = CByte(tempcol Mod 256)
                    tempcol = CInt((tempcol - Redval) / 256)
                    Greenval = CByte(tempcol Mod 256)
                    tempcol = CInt((tempcol - Greenval) / 256)
                    Blueval = CByte(tempcol Mod 256)
                    If (InnerFrameRed > Redval) Then
                        Redval = CByte(Redval + Round((InnerFrameRed - Redval) * tempfact))
                    Else
                        Redval = CByte(Redval - Round((Redval - InnerFrameRed) * tempfact))
                    End If
                    If (InnerFrameGreen > Greenval) Then
                        Greenval = CByte(Greenval + Round((InnerFrameGreen - Greenval) * tempfact))
                    Else
                        Greenval = CByte(Greenval - Round((Greenval - InnerFrameGreen) * tempfact))
                    End If
                    If (InnerFrameBlue > Blueval) Then
                        Blueval = CByte((Blueval + Round((InnerFrameBlue - Blueval) * tempfact)))
                    Else
                        Blueval = CByte((Blueval - Round((Blueval - InnerFrameBlue) * tempfact)))
                    End If
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (_InnerFrameWidthLower + _MiddleFrameWidthLower + _OuterFrameWidthLower)) = Redval + (Greenval * (256)) + (Blueval * (65536))
                Next
            Next
        End Sub

#Region "Filter Properties"
        Private _FrameType As FrameTypes = FrameTypes.NoFrameCrossing

        ''' <summary>
        '''     Gets or sets the style of the frame effect
        ''' </summary>
        ''' <returns></returns>
        Public Property FrameType() As FrameTypes
            Get
                Return _FrameType
            End Get
            Set(ByVal Value As FrameTypes)
                _FrameType = Value
            End Set
        End Property


        ''' <summary>
        '''     Width of each of the four sides of the outer frame
        '''     No frame will be drawn on a side which is set to 0
        ''' </summary>
        Protected _OuterFrameWidthLeft As Integer
        Protected _OuterFrameWidthRight As Integer
        Protected _OuterFrameWidthUpper As Integer
        Protected _OuterFrameWidthLower As Integer


        ''' <summary>
        '''     Width of each of the four sides of the middle frame
        '''     No frame will be drawn on a side which is set to 0
        ''' </summary>
        Protected _MiddleFrameWidthLeft As Integer
        Protected _MiddleFrameWidthRight As Integer
        Protected _MiddleFrameWidthUpper As Integer
        Protected _MiddleFrameWidthLower As Integer


        ''' <summary>
        '''     Width of each of the four sides of the inner frame
        '''     No frame will be drawn on a side which is set to 0
        ''' </summary>
        Protected _InnerFrameWidthLeft As Integer
        Protected _InnerFrameWidthRight As Integer
        Protected _InnerFrameWidthUpper As Integer
        Protected _InnerFrameWidthLower As Integer


        ''' <summary>
        '''     The Starting and ending values of the gradient's transparency,
        '''     allows for a hard step or edge in transparency or only slightly fadeout (not
        '''     only complete ones as before).
        ''' </summary>
        Protected _InnerFrameGradientCoverageStartValue As Double = 0
        Protected _InnerFrameGradientCoverageEndValue As Double = 1
        Protected _InnerFrameGradientFadeModifier As Double = 1.6

        Private _OuterFrameBlue As Byte

        ''' <summary>
        '''     Blue colour component of outer frame's colour
        ''' </summary>
        Public Property OuterFrameBlue() As Byte
            Get
                Return _OuterFrameBlue
            End Get
            Set(ByVal Value As Byte)
                _OuterFrameBlue = Value
            End Set
        End Property

        Private _OuterFrameGreen As Byte

        ''' <summary>
        '''     Green colour component of outer frame's colour
        ''' </summary>
        Public Property OuterFrameGreen() As Byte
            Get
                Return _OuterFrameGreen
            End Get
            Set(ByVal Value As Byte)
                _OuterFrameGreen = Value
            End Set
        End Property

        Private _OuterFrameRed As Byte

        ''' <summary>
        '''     Red colour component of outer frame's colour
        ''' </summary>
        Public Property OuterFrameRed() As Byte
            Get
                Return _OuterFrameRed
            End Get
            Set(ByVal Value As Byte)
                _OuterFrameRed = Value
            End Set
        End Property


        Private _InnerFrameBlue As Byte

        ''' <summary>
        '''     Blue colour component of inner frame's colour
        ''' </summary>
        Public Property InnerFrameBlue() As Byte
            Get
                Return _InnerFrameBlue
            End Get
            Set(ByVal Value As Byte)
                _InnerFrameBlue = Value
            End Set
        End Property

        Private _InnerFrameGreen As Byte

        ''' <summary>
        '''     Green colour component of inner frame's colour
        ''' </summary>
        Public Property InnerFrameGreen() As Byte
            Get
                Return _InnerFrameGreen
            End Get
            Set(ByVal Value As Byte)
                _InnerFrameGreen = Value
            End Set
        End Property

        Private _InnerFrameRed As Byte

        ''' <summary>
        '''     Red colour component of inner frame's colour
        ''' </summary>
        Public Property InnerFrameRed() As Byte
            Get
                Return _InnerFrameRed
            End Get
            Set(ByVal Value As Byte)
                _InnerFrameRed = Value
            End Set
        End Property


        Private _MiddleFrameBlue As Byte

        ''' <summary>
        '''     Blue colour component of middle frame's colour
        ''' </summary>
        Public Property MiddleFrameBlue() As Byte
            Get
                Return _MiddleFrameBlue
            End Get
            Set(ByVal Value As Byte)
                _MiddleFrameBlue = Value
            End Set
        End Property

        Private _MiddleFrameGreen As Byte

        ''' <summary>
        '''     Green colour component of middle frame's colour
        ''' </summary>
        Public Property MiddleFrameGreen() As Byte
            Get
                Return _MiddleFrameGreen
            End Get
            Set(ByVal Value As Byte)
                _MiddleFrameGreen = Value
            End Set
        End Property

        Private _MiddleFrameRed As Byte

        ''' <summary>
        '''     Red colour component of middle frame's colour
        ''' </summary>
        Public Property MiddleFrameRed() As Byte
            Get
                Return _MiddleFrameRed
            End Get
            Set(ByVal Value As Byte)
                _MiddleFrameRed = Value
            End Set
        End Property


        ''' <summary>
        '''     Integer colour value of inner frame's colour
        ''' </summary>
        Public Property InnerFrameColor() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(0, Me.InnerFrameRed, Me.InnerFrameGreen, Me.InnerFrameBlue)
            End Get
            Set(ByVal Value As System.Drawing.Color)
                Dim color As System.Drawing.Color = Value
                Me.InnerFrameRed = color.R
                Me.InnerFrameGreen = color.G
                Me.InnerFrameBlue = color.B
            End Set
        End Property


        ''' <summary>
        '''     Integer colour value of middle frame's colour
        ''' </summary>
        Public Property MiddleFrameColor() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(0, Me.MiddleFrameRed, Me.MiddleFrameGreen, Me.MiddleFrameBlue)
            End Get
            Set(ByVal Value As System.Drawing.Color)
                Dim color As System.Drawing.Color = Value
                Me.MiddleFrameRed = color.R
                Me.MiddleFrameGreen = color.G
                Me.MiddleFrameBlue = color.B
            End Set
        End Property


        ''' <summary>
        '''     Integer colour value of outer frame's colour
        ''' </summary>
        Public Property OuterFrameColor() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(1, Me.OuterFrameRed, Me.OuterFrameGreen, Me.OuterFrameBlue)
            End Get
            Set(ByVal Value As System.Drawing.Color)
                Dim color As System.Drawing.Color = Value
                Me.OuterFrameRed = color.R
                Me.OuterFrameGreen = color.G
                Me.OuterFrameBlue = color.B
            End Set
        End Property

#End Region

    End Class

End Namespace