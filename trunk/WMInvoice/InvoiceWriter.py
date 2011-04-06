#!/usr/bin/env python

import PO
import string
from datetime import datetime, date, time
import decimal
import InvTemplates

class InvoiceWriter():
    buffer = ''
    poList = {}
    currentLine = 0
    out = {}
    isaCntlNo = 2
    groupCntlNo = 2
    setControlNo = 0
    segmentCount = 0
    totalInv = 0
    totalUnits = 0
    linesInInv = 0
    transactionSets = 0
    shortTime  = '1315'
    tp = InvTemplates.InvTemplates()
    def __init__(self,list):
        transactionSets = 0
        self.setDates()
        self.poList = list
        tp = InvTemplates.InvTemplates()
        self.currentPO = {}
        self.writeFile()

    def writeFile(self):
        fileOut = 'wmbatch3.out'
        dir = 'E:/data/calAccessories/data/edi/WM/invoiceInput/'
        invOut = dir + fileOut
        self.out = open(invOut, 'w')
        self.wISA()
        self.wGS()
        self.segmentCount = 0
        self.totalInv = 0
        self.totalUnits = 0
        self.linesInInv = 0
        for po in self.poList.keys():
            self.segmentCount = 0
            self.totalInv = 0
            self.totalUnits = 0
            self.linesInInv = 0
            self.currentPO = self.poList[po]
            self.wST()
            self.wBIG()
            self.wREF()
            self.ourAddress()
            self.shipToID()
            self.wITD()
            self.wDTM()
            self.wFOB()
            for line in self.currentPO.poLinesDict:
                self.currentPOLine = self.currentPO.poLinesDict[line]
                self.wIT1()
                self.wPID()
                self.wPO4a()
            self.wTDS()
            self.wISS()
            self.wCTT()
            self.wSE()
        self.wGE()
        self.wIEA()
            
    def wISA(self):
        isa = self.tp.getHeadTemplate('ISA')
        isaTp = string.Template(isa)
        shortDay = self.date
        shortTime = self.time
        
        cntlNo = self.isaCntlNo
        padCntlNo = '%(num)09d' % {"num":cntlNo}
        fIsa = isaTp.substitute(date=shortDay,time=shortTime,controlNo=padCntlNo)
        print >>self.out,  fIsa
            
    def wGS(self):
        gs = self.tp.getHeadTemplate('GS')
        gsTp = string.Template(gs)
        date = self.fullDate
        shortTime = self.time
        grpCntlNo = self.groupCntlNo
        padCntlNo = '%(num)04d' % {"num":grpCntlNo}
        fGS = gsTp.substitute(fullDate=date,time=shortTime,groupCtlNo=padCntlNo)
        print >>self.out,  fGS
        
    def wST(self):
        st = self.tp.getHeadTemplate('ST')
        stTp = string.Template(st)
        self.setControlNo = self.setControlNo + 1
        setCntlNo = self.setControlNo
        padCntlNo = '%(num)04d' % {"num":setCntlNo}
        fST = stTp.substitute(setControlNo=padCntlNo)
        print >>self.out,  fST
        self.transactionSets = self.transactionSets + 1
        self.IncrementSeg()
        
    def wBIG(self):
        big = self.tp.getHeadTemplate('BIG')
        bigTp = string.Template(big)
        invDte = self.currentPO.getInvoiceDate()
        invNum = self.currentPO.getInvoiceNum()
        poDte  = self.currentPO.getPoDate()
        poNum  = self.currentPO.getPoNumber()
        fBig = bigTp.substitute(invDate=invDte,invNo=invNum,poDate=poDte,poNo=poNum)
        print >>self.out,  fBig
        self.IncrementSeg()
    
    def wIT1(self):
        it1 = self.tp.getHeadTemplate('IT1')
        it1Tp = string.Template(it1)
        # decimal.getcontext().prec = 4
        qty = decimal.Decimal(self.currentPOLine['shipQty'])
        price = decimal.Decimal(self.currentPOLine['unitPrice'])
        lineTotal = qty * price
        self.totalInv = self.totalInv + lineTotal
        self.totalUnits = self.totalUnits + qty
        self.linesInInv = self.linesInInv + 1
        wmUpc = self.currentPOLine['wmUpc']
        wmItem = self.currentPOLine['wmPart']
        strQty = '%(#)03d' % {"#":qty}
        fIt1  = it1Tp.substitute(qtyInvoiced=qty,unitPrice=price,upc=wmUpc,buyerItem=wmItem)   
        # IT1**$qtyInvoiced*CA*$unitPrice**UP*$upc*IN*$buyerItem~',
        print >>self.out,  fIt1
        self.IncrementSeg()
    
    def wPID(self):
        pid = self.tp.getHeadTemplate('PID')
        pidTp = string.Template(pid)
        descr = self.currentPOLine['partDescr']
        fPid  = pidTp.substitute(itemDescr=descr)
        # PID*F****$itemDescr~
        print >>self.out,  fPid
        self.IncrementSeg()
    
    def wPO4a(self):
        po4 = self.tp.getHeadTemplate('PO4a')
        po4Tp = string.Template(po4)
        packSize = self.currentPOLine['packSize']
        fPo4 = po4Tp.substitute(pack=packSize)
        # PO4*$pack~
        print >>self.out, fPo4
        self.IncrementSeg()
    
    def wTDS(self):
        tds = self.tp.getHeadTemplate('TDS')
        tdsTp = string.Template(tds)
        total = int(self.totalInv * 100)
        fTds = tdsTp.substitute(totalInv=total)
        # TDS*$totalInv~
        print >>self.out, fTds
        self.IncrementSeg()

    def wISS(self):
        iss = self.tp.getHeadTemplate('ISS')
        issTp = string.Template(iss)
        units = int(self.totalUnits)
        fIss = issTp.substitute(totalUnits=units)
        # ISS*$totalUnits*CA~',
        print >>self.out, fIss
        self.IncrementSeg()
    
    def wCTT(self):
        ctt = self.tp.getHeadTemplate('CTT')
        cttTp = string.Template(ctt)
        nLines = self.linesInInv
        fCtt = cttTp.substitute(nLineItems=nLines)
        # CTT*$nLineItems~',
        print >>self.out, fCtt
        self.IncrementSeg()
    
    def wSE(self):
        se  = self.tp.getHeadTemplate('SE')
        seTp = string.Template(se)
        # SE*$totalSegments*$seControlNo~
        segmentCount = self.segmentCount + 1
        self.segmentCount = 0
        cntrlNum  = self.setControlNo
        padCntlNo = '%(num)04d' % {"num":cntrlNum}
        fSe = seTp.substitute(totalSegments=segmentCount,seControlNo=padCntlNo)
        print >>self.out, fSe
        
    def wGE(self):
        ge  = self.tp.getHeadTemplate('GE')
        geTp = string.Template(ge)
        transactionSets = self.transactionSets
        grpCntl = self.groupCntlNo
        padCntlNo = '%(num)04d' % {"num":grpCntl}
        fGe = geTp.substitute(tranSets=transactionSets,groupCtlNo=padCntlNo)
        print >>self.out, fGe
        #GE*$tranSets*$groupCtlNo~',
    
    def wIEA(self):
        iea  = self.tp.getHeadTemplate('IEA')
        ieaTp = string.Template(iea)
        cntlNo = self.isaCntlNo
        padCntlNo = '%(num)09d' % {"num":cntlNo}
        fIea = ieaTp.substitute(controlNo=padCntlNo)
        # IEA*1*$controlNo~
        print >>self.out, fIea
        
    def wREF(self):
        ref = self.tp.getHeadTemplate('REFIA')
        print >>self.out,  ref
        self.IncrementSeg()

    def wITD(self):
        itd = self.tp.getHeadTemplate('ITD')
        print >>self.out, itd
        self.IncrementSeg()
    
    def wFOB(self):
        fob = self.tp.getHeadTemplate('FOB')
        print >>self.out, fob
        self.IncrementSeg()
        
    def ourAddress(self):
        n1su = self.tp.getHeadTemplate('N1SU')
        print >>self.out,  n1su
        self.IncrementSeg()

        n3su = self.tp.getHeadTemplate('N3SU')
        print >>self.out,  n3su
        self.IncrementSeg()

        n4su = self.tp.getHeadTemplate('N4SU')
        print >>self.out,  n4su
        self.IncrementSeg()
    
    def shipToID(self):
        n1st = self.tp.getHeadTemplate('N1ST')
        n1StTp = string.Template(n1st)
        shipToId = self.currentPO.getShipToNum()
        gln = self.currentPO.getGln()
        fN1st = n1StTp.substitute(shipTo=shipToId,glnNo=gln)
        print >>self.out, fN1st
        self.IncrementSeg()
    
    def wDTM(self):
        dtm  = self.tp.getHeadTemplate('DTM')
        dtmTp =  string.Template(dtm)
        shipDte = self.currentPO.getInvoiceDate()
        fDtm = dtmTp.substitute(shipDate=shipDte)
        print >>self.out, fDtm
        self.IncrementSeg()
    
    def IncrementSeg(self):
        self.segmentCount = self.segmentCount + 1

    def setDates(self):
        dt =  datetime.now()
        self.date = dt.strftime("%y%m%d")
        self.fullDate = dt.strftime("%Y%m%d")
        tt = dt.timetuple()
        self.time = '%(#)02d' %{"#":tt[3]} + '%(#)02d' %{"#":tt[4]}
