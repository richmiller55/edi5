#!/usr/bin/env python

import GlnLookup
import PartXRef

class PO():
    poHash = {}
    poLinesDict = {}
    currentLine = {}
    getDc = {}
    poLinesDict = {}
    def __init__(self):
        self.DCGln = GlnLookup.GlnToDC()
        self.partXref = PartXRef.PartXRef()
        self.currentLine = {}
        self.poHash = {}
        self.poLinesDict = {}

    def postLine(self):
        self.poLinesDict[self.currentLine['orderLine']] = self.currentLine     
        self.currentLine = {}

    def setPoNum(self,poNum):
        self.poHash['poNum'] = poNum

    def setOrderDate(self,orderDate):
        self.poHash['PODate'] = orderDate
    
    def setOpenOrderFlag(self,openOrder):
        self.poHash['openOrderFlag'] = openOrder

    def setTotalInvoiced(self,totalInvoiced):
        self.poHash['totalInvoiced'] = totalInvoiced
    
    def setShipToNum(self,shipToNum):
        self.poHash['shipToNum'] = shipToNum
        self.poHash['gln'] = self.DCGln.glnLookup(shipToNum)
   
    def setInvoiceNum(self,invoiceNum):
        self.poHash['invoiceNum'] = invoiceNum

    def setInvoiceDate(self,invoiceDate):
        self.poHash['invoiceDate'] = invoiceDate

    def setOrderLine(self,orderLine):
        self.currentLine['orderLine'] = orderLine
   
    def setOrderQty(self,orderQty):
        self.currentLine['orderQty'] = orderQty
   
    def setPart(self,partNum):
        self.currentLine['caUpc'] = partNum
        self.currentLine['wmUpc'] = self.partXref.getWmUpc(partNum)
        self.currentLine['wmPart'] = self.partXref.getWmPart(partNum)
        self.currentLine['packSize'] = self.partXref.getPackSize(partNum)

    def setShipQty(self,shipQty):
        self.currentLine['shipQty'] = shipQty

    def setPartDescr(self,partDescr):
        self.currentLine['partDescr'] = partDescr

    def setUnitPrice(self,unitPrice):
        self.currentLine['unitPrice'] = unitPrice

    def getHash(self):
        return self.poHash
 
    def getPoLineDict(self):
        return self.poLineDict
    
    def getPoNumber(self):
        return self.poHash['poNum']

    def getGln(self):
        return self.poHash['gln']
    
    def getPoDate(self):
        return self.poHash['PODate']

    def getOrderDate(self):
        return self.poHash['PODate']
        
    def getInvoiceDate(self):
        return self.poHash['invoiceDate']

    def getInvoiceNum(self):
        return self.poHash['invoiceNum']

    def getShipToNum(self):
        return self.poHash['shipToNum']

    
    
