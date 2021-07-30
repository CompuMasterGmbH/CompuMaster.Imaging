'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Math

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A class which includes all the functionality for applying different
    '''     shadow effects to an image.
    ''' </summary>
    Public Class ShadowMaker
        Inherits ImageFilterBase


        ''' <summary>
        '''     Creates the outer frame around the image on which the shadow effect
        '''     will be drawn
        ''' </summary>
        Protected Sub ApplyOuterFrame()
            Dim BackgroundFarbe As Integer
            ' Außenrand (Backgroundfarbe)
            BackgroundFarbe = BackgroundRed + (BackgroundGreen * (256)) + (BackgroundBlue * (65536))
            For Counter1 As Integer = 0 To Image.Height - 1
                For Counter2 As Integer = 0 To FrameWidth - 1   ' linker Rand
                    ImagePixelColorValue(Counter2, Counter1) = BackgroundFarbe
                Next
                For Counter2 As Integer = 0 To FrameWidth + ShadowWidth + ShadowGradientWidth - 1   ' rechter Rand
                    ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth), Counter1) = BackgroundFarbe
                Next
            Next
            For Counter1 As Integer = 0 To Image.Width - 1
                For Counter2 As Integer = 0 To FrameWidth - 1   ' oberer Rand
                    ImagePixelColorValue(Counter1, Counter2) = BackgroundFarbe
                Next
                For Counter2 As Integer = 0 To FrameWidth + ShadowWidth + ShadowGradientWidth - 1   ' unterer Rand
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth)) = BackgroundFarbe
                Next
            Next
        End Sub


        ''' <summary>
        '''     Calculates the integer colour value of a single shadow pixel
        '''     dependant on the background colour behind it, the shadow's colour
        '''     and the transparency factor of the shadow
        ''' </summary>
        ''' <param name="oldColor"></param>
        ''' <param name="transparencyFactor"></param>
        Protected Function CalculateShadowPixelColor(ByVal oldColor As Integer, ByVal transparencyFactor As Double) As Integer
            Dim tempcol As Integer = oldColor
            Dim Blueval As Byte
            Dim Greenval As Byte
            Dim Redval As Byte
            Redval = CByte(tempcol Mod 256)
            tempcol = CInt((tempcol - Redval) / 256)
            Greenval = CByte(tempcol Mod 256)
            tempcol = CInt((tempcol - Greenval) / 256)
            Blueval = CByte(tempcol Mod 256)
            If (ShadowRed > Redval) Then
                Redval = CByte(Redval + Round((ShadowRed - Redval) * transparencyFactor))
            Else
                Redval = CByte(Redval - Round((Redval - ShadowRed) * transparencyFactor))
            End If
            If (ShadowGreen > Greenval) Then
                Greenval = CByte(Greenval + Round((ShadowGreen - Greenval) * transparencyFactor))
            Else
                Greenval = CByte(Greenval - Round((Greenval - ShadowGreen) * transparencyFactor))
            End If
            If (ShadowBlue > Blueval) Then
                Blueval = CByte(Blueval + Round((ShadowBlue - Blueval) * transparencyFactor))
            Else
                Blueval = CByte(Blueval - Round((Blueval - ShadowBlue) * transparencyFactor))
            End If
            Return Redval + (Greenval * (256)) + (Blueval * (65536))
        End Function


        ''' <summary>
        '''     Draws the shadow effect around the image onto the outer frame
        ''' </summary>
        Protected Sub ApplyShadow()
            Dim Startval As Integer
            Dim Endval As Integer
            Dim tempcol As Integer
            Dim tempfact As Double
            ' Shadow
            tempfact = ShadowStrength / 100
            Startval = FrameWidth + ShadowWidth
            Endval = Image.Width - (FrameWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' unterer Shadow
                For Counter2 As Integer = 0 To ShadowWidth - 1
                    tempcol = ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth))
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth)) = CalculateShadowPixelColor(tempcol, tempfact)
                Next
            Next
            Startval = FrameWidth + ShadowWidth
            Endval = Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' rechter Shadow
                For Counter2 As Integer = 0 To ShadowWidth - 1
                    tempcol = ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth), Counter1)
                    ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth), Counter1) = CalculateShadowPixelColor(tempcol, tempfact)
                Next
            Next
        End Sub


        ''' <summary>
        '''     Calculates the integer colour value of a single shadow gradient pixel
        '''     dependant on the background colour behind it, the shadow's colour
        '''     and the transparency factor of the shadow gradient at the current pixel
        ''' </summary>
        ''' <param name="oldColor"></param>
        ''' <param name="transparencyFactor"></param>
        Protected Function CalculateShadowGradientPixelColor(ByVal oldColor As Integer, ByVal transparencyFactor As Double) As Integer
            Dim tempcol As Integer = oldColor
            Dim Blueval As Byte
            Dim Greenval As Byte
            Dim Redval As Byte
            Redval = CByte(tempcol Mod 256)
            tempcol = CInt((tempcol - Redval) / 256)
            Greenval = CByte(tempcol Mod 256)
            tempcol = CInt((tempcol - Greenval) / 256)
            Blueval = CByte(tempcol Mod 256)
            If (ShadowRed > Redval) Then
                Redval = CByte(Redval + Round((ShadowRed - Redval) * transparencyFactor * (ShadowStrength / 100)))
            Else
                Redval = CByte(Redval - Round((Redval - ShadowRed) * transparencyFactor * (ShadowStrength / 100)))
            End If
            If (ShadowGreen > Greenval) Then
                Greenval = CByte(Greenval + Round((ShadowGreen - Greenval) * transparencyFactor * (ShadowStrength / 100)))
            Else
                Greenval = CByte(Greenval - Round((Greenval - ShadowGreen) * transparencyFactor * (ShadowStrength / 100)))
            End If
            If (ShadowBlue > Blueval) Then
                Blueval = CByte(Blueval + Round((ShadowBlue - Blueval) * transparencyFactor * (ShadowStrength / 100)))
            Else
                Blueval = CByte(Blueval - Round((Blueval - ShadowBlue) * transparencyFactor * (ShadowStrength / 100)))
            End If
            Return Redval + (Greenval * (256)) + (Blueval * (65536))
        End Function


        ''' <summary>
        '''     Draws a shadow gradient effect (fading out) around the main shadow
        ''' </summary>
        Protected Sub ApplyShadowGradient()
            Dim Startval As Integer
            Dim Endval As Integer
            Dim tempcol As Integer
            Dim tempfact As Double
            ' Shadowverlauf
            Startval = FrameWidth + ShadowWidth
            Endval = Image.Width - (FrameWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' unterer Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowGradientWidth))
                    ImagePixelColorValue(Counter1, Counter2 + Image.Height - (FrameWidth + ShadowGradientWidth)) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            Startval = FrameWidth + ShadowWidth
            Endval = Image.Height - (FrameWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' rechter Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowGradientWidth), Counter1)
                    ImagePixelColorValue(Counter2 + Image.Width - (FrameWidth + ShadowGradientWidth), Counter1) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            Startval = Image.Width - (FrameWidth + ShadowWidth + ShadowGradientWidth)
            Endval = Image.Width - (FrameWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' oberer Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(Counter1, FrameWidth + ShadowWidth - (Counter2 + 1))
                    ImagePixelColorValue(Counter1, FrameWidth + ShadowWidth - (Counter2 + 1)) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            Startval = Image.Height - (FrameWidth + ShadowWidth + ShadowGradientWidth)
            Endval = Image.Height - (FrameWidth + ShadowGradientWidth + 1)
            For Counter1 As Integer = Startval To Endval   ' linker Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(FrameWidth + ShadowWidth - (Counter2 + 1), Counter1)
                    ImagePixelColorValue(FrameWidth + ShadowWidth - (Counter2 + 1), Counter1) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            For Counter1 As Integer = 0 To ShadowGradientWidth - 1 ' linker unterer Eck-Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempfact *= ((ShadowGradientWidth + 1) - (Counter1 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(FrameWidth + ShadowWidth - 1 - Counter2, Image.Height - (FrameWidth + ShadowGradientWidth) + Counter1)
                    ImagePixelColorValue(FrameWidth + ShadowWidth - 1 - Counter2, Image.Height - (FrameWidth + ShadowGradientWidth) + Counter1) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            For Counter1 As Integer = 0 To ShadowGradientWidth - 1 ' rechter unterer Eck-Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempfact *= ((ShadowGradientWidth + 1) - (Counter1 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(Image.Width - (FrameWidth + ShadowGradientWidth) + Counter2, Image.Height - (FrameWidth + ShadowGradientWidth) + Counter1)
                    ImagePixelColorValue(Image.Width - (FrameWidth + ShadowGradientWidth) + Counter2, Image.Height - (FrameWidth + ShadowGradientWidth) + Counter1) = CalculateShadowGradientPixelColor(tempcol, tempfact)
                Next
            Next
            For Counter1 As Integer = 0 To ShadowGradientWidth - 1 ' rechter oberer Eck-Shadowverlauf
                For Counter2 As Integer = 0 To ShadowGradientWidth - 1
                    tempfact = ((ShadowGradientWidth + 1) - (Counter2 + 1)) / (ShadowGradientWidth + 1)
                    tempfact *= ((ShadowGradientWidth + 1) - (Counter1 + 1)) / (ShadowGradientWidth + 1)
                    tempcol = ImagePixelColorValue(Image.Width - (FrameWidth + ShadowGradientWidth) + Counter2, FrameWidth + ShadowWidth - 1 - Counter1)
                    ImagePixelColorValue(Image.Width - (FrameWidth + ShadowGradientWidth) + Counter2, FrameWidth + ShadowWidth - 1 - Counter1) = CalculateShadowGradientPixelColor(tempcol, tempfact)
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
                ApplyOuterFrame()
                ApplyShadow()
                ApplyShadowGradient()
            End If
        End Sub

#Region "Filter Properties"
        Private _FrameWidth As Integer

        ''' <summary>
        '''     The width of the additional border area where the background is visible
        ''' 
        '''     This value does NOT include the additional width of the shadow and the
        '''     shadow gradient, it DOES only describes the width of the border area
        '''     around the image, shadow and shadow gradient
        ''' 
        '''     May be zero even if a shadow and a shadow gradient are drawn
        ''' </summary>
        Public Property FrameWidth() As Integer
            Get
                Return _FrameWidth
            End Get
            Set(ByVal Value As Integer)
                _FrameWidth = Value
            End Set
        End Property

        Private _ShadowWidth As Integer

        ''' <summary>
        '''     Gets or sets the width that the shadow will be drawn below and
        '''     to the right of the main image's area
        ''' </summary>
        Public Property ShadowWidth() As Integer
            Get
                Return _ShadowWidth
            End Get
            Set(ByVal Value As Integer)
                _ShadowWidth = Value
            End Set
        End Property

        Private _ShadowGradientWidth As Integer

        ''' <summary>
        '''     Gets or sets the width of the shadow gradient to be drawn around
        '''     the shadow
        ''' 
        '''     This value must be &lt;= than the shadow's width
        ''' </summary>
        Public Property ShadowGradientWidth() As Integer
            Get
                Return _ShadowGradientWidth
            End Get
            Set(ByVal Value As Integer)
                _ShadowGradientWidth = Value
            End Set
        End Property

        Private _ShadowStrength As Byte

        ''' <summary>
        '''     Gets or sets the coverage (inverse transparency) factor (in %)
        '''     of the shadow
        ''' </summary>
        Public Property ShadowStrength() As Byte
            Get
                Return _ShadowStrength
            End Get
            Set(ByVal Value As Byte)
                _ShadowStrength = Value
            End Set
        End Property

        Private _BackgroundBlue As Byte

        ''' <summary>
        '''     Gets or sets the blue colour component of the background frame's colour
        ''' </summary>
        Public Property BackgroundBlue() As Byte
            Get
                Return _BackgroundBlue
            End Get
            Set(ByVal Value As Byte)
                _BackgroundBlue = Value
            End Set
        End Property

        Private _BackgroundRed As Byte

        ''' <summary>
        '''     Gets or sets the red colour component of the background frame's colour
        ''' </summary>
        Public Property BackgroundRed() As Byte
            Get
                Return _BackgroundRed
            End Get
            Set(ByVal Value As Byte)
                _BackgroundRed = Value
            End Set
        End Property

        Private _BackgroundGreen As Byte

        ''' <summary>
        '''     Gets or sets the green colour component of the background frame's colour
        ''' </summary>
        Public Property BackgroundGreen() As Byte
            Get
                Return _BackgroundGreen
            End Get
            Set(ByVal Value As Byte)
                _BackgroundGreen = Value
            End Set
        End Property

        Private _ShadowBlue As Byte

        ''' <summary>
        '''     Gets or sets the blue colour component of the shadow's colour
        ''' </summary>
        Public Property ShadowBlue() As Byte
            Get
                Return _ShadowBlue
            End Get
            Set(ByVal Value As Byte)
                _ShadowBlue = Value
            End Set
        End Property

        Private _ShadowRed As Byte

        ''' <summary>
        '''     Gets or sets the red colour component of the shadow's colour
        ''' </summary>
        Public Property ShadowRed() As Byte
            Get
                Return _ShadowRed
            End Get
            Set(ByVal Value As Byte)
                _ShadowRed = Value
            End Set
        End Property

        Private _ShadowGreen As Byte

        ''' <summary>
        '''     Gets or sets the green colour component of the shadow's colour
        ''' </summary>
        Public Property ShadowGreen() As Byte
            Get
                Return _ShadowGreen
            End Get
            Set(ByVal Value As Byte)
                _ShadowGreen = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the shadow's 24bit integer colour
        ''' </summary>
        Public Property ShadowColor() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(0, Me.ShadowRed, Me.ShadowGreen, Me.ShadowBlue)
            End Get
            Set(ByVal Value As System.Drawing.Color)
                Dim color As System.Drawing.Color = Value
                Me.ShadowRed = color.R
                Me.ShadowGreen = color.G
                Me.ShadowBlue = color.B
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the background's 24bit integer colour
        ''' </summary>
        Public Property BackgroundColor() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(0, Me.BackgroundRed, Me.BackgroundGreen, Me.BackgroundBlue)
            End Get
            Set(ByVal Value As System.Drawing.Color)
                Dim color As System.Drawing.Color = Value
                Me.BackgroundRed = color.R
                Me.BackgroundGreen = color.G
                Me.BackgroundBlue = color.B
            End Set
        End Property
#End Region


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