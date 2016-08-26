'Copyright 2006,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A class which includes all the functionality for applying different
    '''     frame effects to an image.
    ''' </summary>
    Public Class FrameMakerAdvanced
        Inherits FrameMakerBase


        ''' <summary>
        '''     Gets or sets the width of the left side of the outer frame
        '''     No outer frame is drawn on this side if set to 0
        ''' </summary>
        Public Property OuterFrameWidthLeft() As Integer
            Get
                Return _OuterFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _OuterFrameWidthLeft = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the right side of the outer frame
        '''     No outer frame is drawn on this side if set to 0
        ''' </summary>
        Public Property OuterFrameWidthRight() As Integer
            Get
                Return _OuterFrameWidthRight
            End Get
            Set(ByVal Value As Integer)
                _OuterFrameWidthRight = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the upper side of the outer frame
        '''     No outer frame is drawn on this side if set to 0
        ''' </summary>
        Public Property OuterFrameWidthUpper() As Integer
            Get
                Return _OuterFrameWidthUpper
            End Get
            Set(ByVal Value As Integer)
                _OuterFrameWidthUpper = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the lower side of the outer frame
        '''     No outer frame is drawn on this side if set to 0
        ''' </summary>
        Public Property OuterFrameWidthLower() As Integer
            Get
                Return _OuterFrameWidthLower
            End Get
            Set(ByVal Value As Integer)
                _OuterFrameWidthLower = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the left side of the middle frame
        '''     No middle frame is drawn on this side if set to 0
        ''' </summary>
        Public Property MiddleFrameWidthLeft() As Integer
            Get
                Return _MiddleFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _MiddleFrameWidthLeft = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the right side of the middle frame
        '''     No middle frame is drawn on this side if set to 0
        ''' </summary>
        Public Property MiddleFrameWidthRight() As Integer
            Get
                Return _MiddleFrameWidthRight
            End Get
            Set(ByVal Value As Integer)
                _MiddleFrameWidthRight = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the upper side of the middle frame
        '''     No middle frame is drawn on this side if set to 0
        ''' </summary>
        Public Property MiddleFrameWidthUpper() As Integer
            Get
                Return _MiddleFrameWidthUpper
            End Get
            Set(ByVal Value As Integer)
                _MiddleFrameWidthUpper = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the lower side of the middle frame
        '''     No middle frame is drawn on this side if set to 0
        ''' </summary>
        Public Property MiddleFrameWidthLower() As Integer
            Get
                Return _MiddleFrameWidthLower
            End Get
            Set(ByVal Value As Integer)
                _MiddleFrameWidthLower = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the left side of the inner frame
        '''     No inner frame is drawn on this side if set to 0
        ''' </summary>
        Public Property InnerFrameWidthLeft() As Integer
            Get
                Return _InnerFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _InnerFrameWidthLeft = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the right side of the inner frame
        '''     No inner frame is drawn on this side if set to 0
        ''' </summary>
        Public Property InnerFrameWidthRight() As Integer
            Get
                Return _InnerFrameWidthRight
            End Get
            Set(ByVal Value As Integer)
                _InnerFrameWidthRight = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the upper side of the inner frame
        '''     No inner frame is drawn on this side if set to 0
        ''' </summary>
        Public Property InnerFrameWidthUpper() As Integer
            Get
                Return _InnerFrameWidthUpper
            End Get
            Set(ByVal Value As Integer)
                _InnerFrameWidthUpper = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the lower side of the inner frame
        '''     No inner frame is drawn on this side if set to 0
        ''' </summary>
        Public Property InnerFrameWidthLower() As Integer
            Get
                Return _InnerFrameWidthLower
            End Get
            Set(ByVal Value As Integer)
                _InnerFrameWidthLower = Value
            End Set
        End Property


        ''' <summary>
        '''     Start coverage value of the inner frame's gradient
        ''' </summary>
        Public Property InnerFrameGradientCoverageStartValue() As Double
            Get
                Return _InnerFrameGradientCoverageStartValue
            End Get
            Set(ByVal Value As Double)
                _InnerFrameGradientCoverageStartValue = Value
            End Set
        End Property


        ''' <summary>
        '''     End coverage value of the inner frame's gradient
        ''' </summary>
        Public Property InnerFrameGradientCoverageEndValue() As Double
            Get
                Return _InnerFrameGradientCoverageEndValue
            End Get
            Set(ByVal Value As Double)
                _InnerFrameGradientCoverageEndValue = Value
            End Set
        End Property


        ''' <summary>
        '''     Modifier for the behaviour of the fade effect,
        '''     &lt; 1:    Main fading moved towards the center of the image
        '''     1:      Linear
        '''     &gt; 1:    Main fading moved towards the edge of the image
        ''' </summary>
        Public Property InnerFrameGradientFadeModifier() As Double
            Get
                Return _InnerFrameGradientFadeModifier
            End Get
            Set(ByVal Value As Double)
                _InnerFrameGradientFadeModifier = Value
            End Set
        End Property

    End Class

End Namespace