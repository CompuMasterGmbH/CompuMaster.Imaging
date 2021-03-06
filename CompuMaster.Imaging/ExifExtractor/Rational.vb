﻿'Copyright 2010,2016 CompuMaster GmbH, http://www.compumaster.de and/or its affiliates. All rights reserved.

Option Explicit On
Option Strict On

Namespace CompuMaster.Drawing.Imaging

    ''' <summary>
    ''' 	Helper class required to do some conversions and the like during the
    ''' 	extraction of EXIF informations.
    ''' </summary>
    Friend Class Rational

        Private n As Integer

        Private d As Integer

        Public Sub New(ByVal n As Integer, ByVal d As Integer)
            Me.n = n
            Me.d = d
            simplify(Me.n, Me.d)
        End Sub

        Public Sub New(ByVal n As UInteger, ByVal d As UInteger)
            Me.n = Convert.ToInt32(n)
            Me.d = Convert.ToInt32(d)
            simplify(Me.n, Me.d)
        End Sub

        Public Sub New()
            Me.n = InlineAssignHelper(Me.d, 0)
        End Sub

        Public Overloads Function ToString(ByVal sp As String) As String
            If sp Is Nothing Then
                sp = Environment.NewLine
            End If
            Return n.ToString() & sp & d.ToString()
        End Function

        Public Function ToDouble() As Double
            If d = 0 Then
                Return 0.0
            End If
            Return Math.Round(Convert.ToDouble(n) / Convert.ToDouble(d), 2)
        End Function

        Private Sub simplify(ByRef a As Integer, ByRef b As Integer)
            If a = 0 OrElse b = 0 Then
                Return
            End If
            Dim gcd As Integer = euclid(a, b)
            a = CType(a / gcd, Integer)
            b = CType(b / gcd, Integer)
        End Sub

        Private Function euclid(ByVal a As Integer, ByVal b As Integer) As Integer
            If b = 0 Then
                Return a
            Else
                Return euclid(b, a Mod b)
            End If
        End Function

        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

    End Class

End Namespace