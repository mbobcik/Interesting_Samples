"""
    Author: Martin Bobcik
    Year: 2020
    Project: Pong
    File: vector2.py
"""

from math import sqrt, sin, cos, radians, degrees, atan2

class Vector2:
    '''
        Vector2 represents vector or position on 2D space.
        This class also provides support function for work
        with vectors.
    '''
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def set(self, x, y):
        '''
        Sets both parts of vector.
        '''
        self.x = x
        self.y = y

    def size(self):
        '''
        Returns size of vector as sqrt(x*x + y*y)
        '''
        return sqrt(pow(self.x, 2) + pow(self.y, 2))

    def normalize(self):
        """
        change the vector to unit vector.
        Unit vector has same direction, and
        it's size is 1.
        """
        size = self.size()
        self.x = self.x / size
        self.y = self.y / size

    def setAngle(self, angle):
        """
        Change vectors direction angle.
        Zero(0) heads to right
        90 heads up
        """
        size = self.size()
        rads = radians(angle)
        self.y = -sin(rads) * size
        self.x = cos(rads) * size

    def getAngle(self):
        """
        Get angle of vector.
        Zero(0) heads to right
        90 heads up
        """
        angle = atan2(-self.y, self.x)
        return degrees(angle)

    def __add__(self, other):
        x = self.x + other.x
        y = self.y + other.y
        return Vector2(x, y)

    def __neg__(self):
        return Vector2(-self.x, -self.y)

    def __str__(self):
        return str(self.x, self.y)
