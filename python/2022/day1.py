#!/usr/bin/env python3

import argparse

parser = argparse.ArgumentParser(description='Advent of Code')

parser.add_argument('part', type=str, help='The day part')

def readInput():
    with open('input-day1.txt') as f:
        lines = [line.rstrip('\n') for line in f]
    return lines

def countElves():
    lines = readInput()
    elves = [0] * 10000
    currentIndex = 0
    for line in lines:
        if line == '':
            currentIndex += 1
            continue
        elves[currentIndex] += int(line)
    elves.sort(reverse=True)
    return elves

def partA():
    print(countElves()[0])

def partB():
    print(sum(countElves()[:3]))

args = parser.parse_args()

if (args.part == 'a'):
    partA()
elif (args.part == 'b'):
    partB()