'Copyright 2010,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    ''' 	Helper class required for ExifExtractor to implement IEnumerable.
    ''' </summary>
    Class ExifExtractorEnumerator
        Implements IEnumerator

        Private exifTable As Hashtable

        Private index As IDictionaryEnumerator

        Friend Sub New(ByVal exif As Hashtable)
            Me.exifTable = exif
            Me.Reset()
            index = exif.GetEnumerator()
        End Sub

        Public Sub Reset() Implements IEnumerator.Reset
            Me.index = Nothing
        End Sub

        Public ReadOnly Property Current() As Object Implements IEnumerator.Current
            Get
                Return (New System.Web.UI.Pair(Me.index.Key, Me.index.Value))
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            If index IsNot Nothing AndAlso index.MoveNext() Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace
