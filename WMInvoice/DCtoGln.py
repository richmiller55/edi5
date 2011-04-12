#!/usr/bin/env python

class DCtoGln():
    DcHash = {'xxx': 0}
    def __init__(self):
        self.init()
    def init(self):        
        retailLinkFile = 'glnLookup.txt'
        dir = '~/src/python/edi/wm/'
        glnfile = dir + retailLinkFile
        input1 = open(glnfile, 'r')
        for line in input1:
            columns = line.split('\t')
            gln = columns[0]
            dc = columns[1]
            self.DcHash[dc] = gln
        input1.close()   
    def glnLookup(self,dc):
        try:
            dcUp = dc.upper()
            gln = self.DcHash[dcUp]
        except:
            print 'gln not on file for this DC  ' + dc
            gln = 'na'
        return gln
