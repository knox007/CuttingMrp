﻿Imports CuttingMrpLib
Imports Repository

Public Class ProcessOrderRepository
    Inherits Repository.Repository(Of ProcessOrder)
    Implements IProcessOrderRepository

    Private _context As CuttingMrpDataContext
    Public Sub New(dc As IDataContextFactory)
        MyBase.New(dc)
        _context = dc.Context
    End Sub

    Public Function Search(conditions As ProcessOrderSearchModel) As IQueryable(Of ProcessOrder) Implements IProcessOrderRepository.Search

        If conditions IsNot Nothing Then
            'Dim processOrders = From o In _context.ProcessOrders
            '                    From b In o.Part.BatchOrderTemplates
            '                    Order By b.orderNr Ascending, o.proceeDate Descending
            '                    Select b, o
            Dim processOrders As IQueryable(Of ProcessOrder) = _context.ProcessOrders

            If Not String.IsNullOrWhiteSpace(conditions.KanbanNr) Then
                processOrders = processOrders.Where(Function(c) c.sourceDoc.Contains(conditions.KanbanNr))
            End If
            ' processOrders = processOrders.Where(Function(c) c.proceeDate >= conditions.ProceeDateFrom And c.proceeDate <= conditions.ProceeDateTo)

            If Not String.IsNullOrWhiteSpace(conditions.OrderNr) Then
                processOrders = processOrders.Where(Function(c) c.orderNr.Contains(conditions.OrderNr))
            End If

            If Not String.IsNullOrWhiteSpace(conditions.DerivedFrom) Then
                processOrders = processOrders.Where(Function(c) c.derivedFrom = conditions.DerivedFrom)
            End If

            If Not String.IsNullOrWhiteSpace(conditions.SourceDoc) Then
                processOrders = processOrders.Where(Function(c) c.sourceDoc.Contains(conditions.SourceDoc))
            End If

            If conditions.ProceeDateFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.proceeDate >= conditions.ProceeDateFrom)
            End If


            If conditions.ProceeDateTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.proceeDate <= conditions.ProceeDateTo)
            End If

            If Not String.IsNullOrWhiteSpace(conditions.PartNrAct) Then
                processOrders = processOrders.Where(Function(c) c.partNr.Equals(conditions.PartNrAct))
            End If

            If Not String.IsNullOrWhiteSpace(conditions.PartNr) Then
                processOrders = processOrders.Where(Function(c) c.partNr.Contains(conditions.PartNr))
            End If

            If conditions.ActualQuantityFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.actualQuantity >= conditions.ActualQuantityFrom)
            End If


            If conditions.ActualQuantityTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.actualQuantity <= conditions.ActualQuantityTo)
            End If


            If conditions.CompleteRateFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.completeRate >= conditions.CompleteRateFrom)
            End If


            If conditions.CompleteRateTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.completeRate <= conditions.CompleteRateTo)
            End If

            If conditions.Status.HasValue Then
                processOrders = processOrders.Where(Function(c) c.status.Equals(conditions.Status))
            End If

            If Not String.IsNullOrWhiteSpace(conditions.MrpRound) Then
                Dim ids As List(Of String) = _context.OrderDerivations.Where(Function(c) c.mrpRound.Equals(conditions.MrpRound)).Select(Function(c) c.orderId).Distinct.ToList
                processOrders = processOrders.Where(Function(c) ids.Contains(c.orderNr))

                'processOrders = processOrders.Where(Function(c) crderDerivations.Where(Function(cc) cc.mrpRound.Equals(conditions.MrpRound)))
            End If

            If conditions.PartType.HasValue Then
                processOrders = processOrders.Where(Function(c) c.Part.partType.Equals(conditions.PartType))
            End If

            If conditions.CreateAtFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.createAt >= conditions.CreateAtFrom)
            End If


            If conditions.CreateAtTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.createAt <= conditions.CreateAtTo)
            End If

            Return processOrders.OrderBy(Function(c) c.sourceDoc).OrderByDescending(Function(c) c.proceeDate)
        Else
            Throw New ArgumentNullException
        End If

    End Function


    Public Function SearchView(conditions As ProcessOrderSearchModel) As IQueryable(Of ProcessOrderView) Implements IProcessOrderRepository.SearchView

        If conditions IsNot Nothing Then
            Dim processOrders As IQueryable(Of ProcessOrderView) = _context.ProcessOrderViews


            ' processOrders = processOrders.Where(Function(c) c.proceeDate >= conditions.ProceeDateFrom And c.proceeDate <= conditions.ProceeDateTo)

            If Not String.IsNullOrWhiteSpace(conditions.OrderNr) Then
                processOrders = processOrders.Where(Function(c) c.orderNr.Contains(conditions.OrderNr))
            End If

            If Not String.IsNullOrWhiteSpace(conditions.DerivedFrom) Then
                processOrders = processOrders.Where(Function(c) c.derivedFrom = conditions.DerivedFrom)
            End If

            If Not String.IsNullOrWhiteSpace(conditions.SourceDoc) Then
                processOrders = processOrders.Where(Function(c) c.sourceDoc.Contains(conditions.SourceDoc))
            End If

            If conditions.ProceeDateFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.proceeDate >= conditions.ProceeDateFrom)
            End If


            If conditions.ProceeDateTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.proceeDate <= conditions.ProceeDateTo)
            End If

            If Not String.IsNullOrWhiteSpace(conditions.PartNr) Then
                processOrders = processOrders.Where(Function(c) c.partNr.Contains(conditions.PartNr))
            End If

            If conditions.ActualQuantityFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.actualQuantity >= conditions.ActualQuantityFrom)
            End If


            If conditions.ActualQuantityTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.actualQuantity <= conditions.ActualQuantityTo)
            End If


            If conditions.CompleteRateFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.completeRate >= conditions.CompleteRateFrom)
            End If


            If conditions.CompleteRateTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.completeRate <= conditions.CompleteRateTo)
            End If

            If conditions.Status.HasValue Then
                processOrders = processOrders.Where(Function(c) c.status.Equals(conditions.Status))
            End If

            If Not String.IsNullOrWhiteSpace(conditions.MrpRound) Then
                Dim ids As List(Of String) = _context.OrderDerivations.Where(Function(c) c.mrpRound.Equals(conditions.MrpRound)).Select(Function(c) c.orderId).Distinct.ToList
                processOrders = processOrders.Where(Function(c) ids.Contains(c.orderNr))

                'processOrders = processOrders.Where(Function(c) crderDerivations.Where(Function(cc) cc.mrpRound.Equals(conditions.MrpRound)))
            End If

            If conditions.PartType.HasValue Then
                processOrders = processOrders.Where(Function(c) c.partType.Equals(conditions.PartType))
            End If

            If conditions.CreateAtFrom.HasValue Then
                processOrders = processOrders.Where(Function(c) c.createAt >= conditions.CreateAtFrom)
            End If


            If conditions.CreateAtTo.HasValue Then
                processOrders = processOrders.Where(Function(c) c.createAt <= conditions.CreateAtTo)
            End If

            Return processOrders.OrderByDescending(Function(c) c.proceeDate)
        Else
            Throw New ArgumentNullException
        End If

    End Function
End Class