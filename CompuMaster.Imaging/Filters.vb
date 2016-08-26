'Copyright 2005,2006,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    'ToDo: All properties with width values should handle the Measurement value
    '1. A measurement switch from absolute to percent and back should always convert all property values from absolute to relative and back
    '2. When measurement is relative, all width property changes have to recalculate the widths from percent to absolute

    ''' <summary>
    '''     Available measurement units
    ''' </summary>
    Public Enum Measurement
        Percent = 0
        Absolute = 1
    End Enum

    ''' <summary>
    '''     Available frame crossing types
    ''' </summary>
    Public Enum FrameTypes As Byte
        NoFrameCrossing = 1
        MiddleFrameCrossesOuterFrame = 2
        InnerFrameCrossesOuterFrames = 3
        MiddleAndInnerFramesCrossesOuterFrames = 4
    End Enum

End Namespace