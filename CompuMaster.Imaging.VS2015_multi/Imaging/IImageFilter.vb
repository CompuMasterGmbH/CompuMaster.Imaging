'Copyright 2005,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    '''     The common interface for all image filters
    ''' </summary>
    Public Interface IImageFilter


        ''' <summary>
        '''     Apply the filter as it has been configured
        ''' </summary>
        Sub ApplyFilter()


        ''' <summary>
        '''     The image where the filter shall be applied
        ''' </summary>
        Property Image() As System.Drawing.Image

    End Interface

End Namespace