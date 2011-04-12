#!/usr/bin/env python

import PO

class POfile():
    buffer = ''
    poList = {}
    currentLine = 0

    def __init__(self):
        self.readInvoiceList()
    
    def getPOList(self):
        return self.poList

    def readInvoiceList(self):
        invoiceData = 'results.txt'
        dir = '~/tmp/'
        invFile = dir + invoiceData
        inv = open(invFile, 'r')
        po = PO.PO()
        lastPoNum = 0
        for line in inv:
            columns = line.split('\t')
            i = 0
            poNum = columns[i]
            i += 1
            if (lastPoNum != poNum and lastPoNum != 0 ):
                self.poList[lastPoNum] = po
                po = PO.PO()

            lastPoNum = poNum
            po.setPoNum(poNum)

            po.setOrderDate(columns[i])
            i += 1
            po.setOpenOrderFlag(columns[i])
            i += 1
            po.setTotalInvoiced(columns[i])
            i += 1
            po.setShipToNum(columns[i])
            i += 1
            po.setOrderLine(columns[i])
            i += 1
            po.setOrderQty(columns[i])
            i += 1
            po.setPart(columns[i])
            i += 1
            po.setPartDescr(columns[i])
            i += 1
            po.setInvoiceNum(columns[i])
            i += 1
            shipToNumInv = columns[i]
            i += 1
            po.setInvoiceDate(columns[i])
            i += 1
            po.setShipQty(columns[i])
            i += 1
            po.setUnitPrice(columns[i])
            po.postLine();

