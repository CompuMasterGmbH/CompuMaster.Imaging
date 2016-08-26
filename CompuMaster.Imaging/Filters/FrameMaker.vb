'Copyright 2005,2006,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     A class which includes all the functionality for applying different
    '''     frame effects to an image.
    ''' </summary>
    Public Class FrameMaker
        Inherits FrameMakerBase


        ''' <summary>
        '''     Gets or sets the width of the outer of up to three frames
        '''     No outer frame is drawn if set to 0
        ''' </summary>
        Public Property OuterFrameWidth() As Integer
            Get
                Return _OuterFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _OuterFrameWidthLeft = Value
                _OuterFrameWidthRight = Value
                _OuterFrameWidthUpper = Value
                _OuterFrameWidthLower = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the middle of up to three frames
        '''     No middle frame is drawn if set to 0
        ''' </summary>
        Public Property MiddleFrameWidth() As Integer
            Get
                Return _MiddleFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _MiddleFrameWidthLeft = Value
                _MiddleFrameWidthRight = Value
                _MiddleFrameWidthUpper = Value
                _MiddleFrameWidthLower = Value
            End Set
        End Property


        ''' <summary>
        '''     Gets or sets the width of the inner of up to three frames
        '''     No inner frame is drawn if set to 0
        ''' </summary>
        Public Property InnerFrameWidth() As Integer
            Get
                Return _InnerFrameWidthLeft
            End Get
            Set(ByVal Value As Integer)
                _InnerFrameWidthLeft = Value
                _InnerFrameWidthRight = Value
                _InnerFrameWidthUpper = Value
                _InnerFrameWidthLower = Value
            End Set
        End Property
    End Class

End Namespace