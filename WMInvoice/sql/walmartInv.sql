--
-- $Id: walmartInv.sql,v 1.1 2008/08/13 22:42:14 co-inet Exp co-inet $
--
--
-- $Log: walmartInv.sql,v $
-- Revision 1.1  2008/08/13 22:42:14  co-inet
-- Initial revision
--
--
--
--
select 
   oh.PONum as PONum,
   oh.OrderDate as OrderDate,
   oh.OpenOrder as OpenOrder,
   oh.TotalInvoiced as TotalInvoiced,
   oh.ShipToNum as ShipToNum,
   od.OrderLine as OrderLine,
   od.OrderQty as OrderQty,
   od.PartNum as PartNum,
   p.PartDescription as PartDescription,
   id.InvoiceNum as InvoiceNum,
   id.ShipToNum as ShipToNum,
   id.InvoiceDate as InvoiceDate,
   id.SellingShipQty as SellingShipQty,
   id.UnitPrice as UnitPrice,
	1 as filler
from orderHed as oh
  left join orderDtl as od
   on oh.OrderNum = od.OrderNum
  left join Customer as cm
    on oh.CustNum = cm.CustNum
  left join Part as p
   on od.PartNum = p.PartNum
  left join InvcDtl as id
   on id.OrderNum = od.OrderNum and
      id.PartNum  = od.PartNum
  left join InvcHead as ih
   on ih.OrderNum = od.OrderNum
where cm.CustID in (90000)   -- come back and add sams later
   and id.InvoiceNum is not null
--   and ih.OpenAmt = ih.InvoiceAmt
   and ih.InvoiceNum >= 799356
--    and ih.InvoiceAmt = ih.OpenAmt
order by oh.PONum,od.OrderLine
