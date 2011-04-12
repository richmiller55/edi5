#!/usr/bin/env python

class PartXRef():
    caUpcToWmPart = {'xxx': 0} # to fill in the IN segment
    caUpcToPackSize = {'xxx': 0}
    def __init__(self):
        self.init()
        # self.caUpcToWmPart = {}
        # self.caUpcToPackSize = {}
    def init(self):        
        file = 'wmXref.txt'
        dir = '/home/co-inet/data/edi/walmart/xref/'
        fullPath = dir + file
        xRef = open(fullPath, 'r')

        for line in xRef:
            columns = line.split('\t')
            i = 0
            caUPC = columns[i]
            i = i + 1
            wmPart = columns[i]
            i = i + 1
            vendDesc = columns[i]
            i = i + 1
            packSize = columns[i]
            self.caUpcToWmPart[caUPC] = wmPart
            self.caUpcToPackSize[caUPC] = packSize
        xRef.close()   
    def getWmPart(self,caUpc):
        try:
            wmPart = self.caUpcToWmPart[caUpc]
        except:
            print 'upc was not found   ' + caUpc
            wmPart = 'na'
        return wmPart
    def getPackSize(self,caUpc):
        try:
            packSize = self.caUpcToPackSize[caUpc]
        except:
            print 'upc was not found   ' + caUpc
            packSize = 'na'
        return packSize
