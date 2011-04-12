#!/usr/bin/env python

import PO

class InvTemplates():
    def __init__(self):
        pass    

    def getHeadTemplate(self,segment):
        return {'BIG':'BIG*$invDate*$invNo*$poDate*$poNo~',
                'REFIA':'REF*IA*890773490~',
            'ISA':'ISA*00*          *00*          *08*890773CL       *08*925485US00     *$date*$time*:*00501*$controlNo*0*P*>~',
                'IEA':'IEA*1*$controlNo~',
                'DTM':'DTM*011*$shipDate~',
                'GS':'GS*IN*890773CL*925485US00*$fullDate*$time*$groupCtlNo*X*005010~',
                'GE':'GE*$tranSets*$groupCtlNo~',
                'N1ST':'N1*ST*Walmart DC $shipTo*UL*$glnNo~',
                'N3ST':'N3*ST*$buyAddress1*$buyAddress2~',
                'N4ST':'N3*ST*$buyCity*$buyState*$buyZip*US~',
                'N1SU':'N1*SU*CALIFORNIA OPTICAL LEATHER~',
                'N3SU':'N3*P.O. BOX 1914~',
                'N4SU':'N4*SAN LEANDRO*CA*94577~',
                'ITD':'ITD*05*15*****35*****35 DAYS~',
                'IT1':'IT1**$qtyInvoiced*CA*$unitPrice**UP*$upc*IN*$buyerItem~',
                'PID':'PID*F****$itemDescr~',
                'PO4a':'PO4*$pack~',
                'PO4b':'PO4*1*************$innerPack~',
                'ISS':'ISS*$totalUnits*CA~',
                'FOB':'FOB*CC~',
                'TDS':'TDS*$totalInv~',
                'CTT':'CTT*$nLineItems~',
                'SE':'SE*$totalSegments*$seControlNo~',
                'ST':'ST*810*$setControlNo~'}[segment]




        
