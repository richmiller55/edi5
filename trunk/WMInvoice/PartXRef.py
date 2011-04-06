#!/usr/bin/env python

class PartXRef():
    
    caUpcToWmUpc = {'xxx': 0}
    # connects our billing record to upc used in error
    # by walmart AK
    caUpcToWmPart = {'xxx': 0} # to fill in the IN segment
    caUpcToPackSize = {'xxx': 0}
    
    def __init__(self):
        self.init()
        
    def init(self):        
        file = 'WMpartXRef.txt'
        dir = 'E:\data\projects\edi\InvFromXML/'
        fullPath = dir + file
        xRef = open(fullPath, 'r')

        for line in xRef:
            columns = line.split('\t')
            i = 0
            caUPC = columns[i]
            i = i + 1
            wmUPC = columns[i]
            i = i + 1
            wmPart = columns[i]
            i = i + 1
            vendDesc = columns[i]
            i = i + 1
            packSize = columns[i]
            self.caUpcToWmUpc[caUPC] = wmUPC
            self.caUpcToWmPart[caUPC] = wmPart
            self.caUpcToPackSize[caUPC] = packSize
        xRef.close()   
            
    def getWmUpc(self,caUpc):
        try:
            wmUpc = self.caUpcToWmUpc[caUpc]
        except:
            print 'upc was not found   ' + caUpc
            wmUpc = caUpc
        return wmUpc

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
