﻿Public Class RequirementSearchModel
    Inherits SearchModelBase


    Private _partNrStr As String
    Private _orderedDateFrom As DateTime?
    Private _orderedDateTo As DateTime?
    Private _requiredTimeFrom As DateTime?
    Private _requiredTimeTo As DateTime?
    Private _quantityFrom As Double?
    Private _quantityTo As Double?
    Private _status As Integer?
    Private _derivedFrom As String
    Private _derivedType As String



    Public Property PartNr As String
        Get
            Return _partNrStr
        End Get
        Set(value As String)
            _partNrStr = value
        End Set
    End Property

    Public Property OrderedDateFrom As DateTime?
        Get
            Return _orderedDateFrom
        End Get
        Set(value As DateTime?)
            _orderedDateFrom = value
        End Set
    End Property

    Public Property OrderedDateTo As DateTime?
        Get
            Return _orderedDateTo
        End Get
        Set(value As DateTime?)
            _orderedDateTo = value
        End Set
    End Property

    Public Property RequiredTimeFrom As DateTime?
        Get
            Return _requiredTimeFrom
        End Get
        Set(value As DateTime?)
            _requiredTimeFrom = value
        End Set
    End Property

    Public Property RequiredTimeTo As DateTime?
        Get
            Return _requiredTimeTo
        End Get
        Set(value As DateTime?)
            _requiredTimeTo = value
        End Set
    End Property

    Public Property QuantityFrom As Double?
        Get
            Return _quantityFrom
        End Get
        Set(value As Double?)
            _quantityFrom = value
        End Set
    End Property

    Public Property QuantityTo As Double?
        Get
            Return _quantityTo
        End Get
        Set(value As Double?)
            _quantityTo = value
        End Set
    End Property

    Public Property Status As Integer?
        Get
            Return _status
        End Get
        Set(value As Integer?)
            _status = value
        End Set
    End Property

    Public Property DerivedFrom As String
        Get
            Return _derivedFrom
        End Get
        Set(value As String)
            _derivedFrom = value
        End Set
    End Property

    Public Property DerivedType As String
        Get
            Return _derivedType
        End Get
        Set(value As String)
            _derivedType = value
        End Set
    End Property

    Public Property OrderNr As String

End Class
