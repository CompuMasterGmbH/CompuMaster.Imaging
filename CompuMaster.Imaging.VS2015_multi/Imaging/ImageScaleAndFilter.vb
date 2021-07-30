'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging

'ToDo: validate following 3 todos and document their results in the namespace summary
'ToDo: verify that the quality keeps top even if the original color number is lesser than true color (pay attention to transparent colors in GIF or PNG files)
'ToDo: verify ability for load/save at least for following file formats: JPG, PNG, GIF, TIF
'ToDo: allocate limitations for file formats (LZW-compressions in GIF+TIF, EPS, WMF, ICO, JPG2000, other common file formats)

'ToDo: Add an inheriting class in camm Media-Manager which allows to specify object numbers 
'ToDo: Create some additional controls inheriting from "ResizedImage" which apply our filters. At least, the samples from the Media-Manager customized resize page should be implemented

Namespace CompuMaster.Drawing.Imaging

    Public Class ImageScaleAndFilter
        Inherits ImageScaling

        Private _FiltersToApplyBeforeResizing As CompuMaster.Drawing.Imaging.IImageFilter()

        ''' <summary>
        '''     An array of filters which shall be applied to the image before it gets resized
        ''' </summary>
        Public Property FiltersToApplyBeforeResizing() As CompuMaster.Drawing.Imaging.IImageFilter()
            Get
                If _FiltersToApplyBeforeResizing Is Nothing Then
                    _FiltersToApplyBeforeResizing = New CompuMaster.Drawing.Imaging.IImageFilter() {}
                End If
                Return _FiltersToApplyBeforeResizing
            End Get
            Set(ByVal Value As CompuMaster.Drawing.Imaging.IImageFilter())
                _FiltersToApplyBeforeResizing = Value
            End Set
        End Property

        Private _FiltersToApplyAfterResizing As CompuMaster.Drawing.Imaging.IImageFilter()

        ''' <summary>
        '''     An array of filters which shall be applied to the image after it has been resized
        ''' </summary>
        Public Property FiltersToApplyAfterResizing() As CompuMaster.Drawing.Imaging.IImageFilter()
            Get
                If _FiltersToApplyAfterResizing Is Nothing Then
                    _FiltersToApplyAfterResizing = New CompuMaster.Drawing.Imaging.IImageFilter() {}
                End If
                Return _FiltersToApplyAfterResizing
            End Get
            Set(ByVal Value As CompuMaster.Drawing.Imaging.IImageFilter())
                _FiltersToApplyAfterResizing = Value
            End Set
        End Property


        ''' <summary>
        '''     Resize the image and update width and height property - if it's not already in the cache of the download handler
        ''' </summary>
        Public Overridable Overloads Sub Resize(ByVal maximumWidth As Integer, ByVal maximumHeight As Integer, ByVal scaleMode As IImageScalingProvider.ScaleMode, ByVal resizeMethod As System.Drawing.Drawing2D.InterpolationMode)

            'File hasn't existed yet, so create it just on the fly
            Dim Result As System.Drawing.Image = CType(Me.ImageInput.Clone, System.Drawing.Image)

            'FiltersToApplyBeforeResizing
            If Not Me.FiltersToApplyBeforeResizing Is Nothing Then
                For Each filter As CompuMaster.Drawing.Imaging.IImageFilter In Me.FiltersToApplyBeforeResizing
                    filter.Image = Result
                    filter.ApplyFilter()
                Next
            End If

            'Resize
            MyBase.Resize(maximumWidth, maximumHeight, scaleMode, resizeMethod)

            'FiltersToApplyAfterResizing
            If Not Me.FiltersToApplyAfterResizing Is Nothing Then
                For Each filter As CompuMaster.Drawing.Imaging.IImageFilter In Me.FiltersToApplyAfterResizing
                    filter.Image = Me.ImageOutput
                    filter.ApplyFilter()
                Next
            End If

        End Sub

    End Class

End Namespace