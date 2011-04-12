#!/usr/bin/env python

import POfile
import InvoiceWriter

def main():
    x = POfile.POfile()
    poList = x.getPOList()
    y = InvoiceWriter.InvoiceWriter(poList)
    
main()    
