

x = [1,2,4,5,2]
print(list(map(lambda y:
  y + 2
, x)))

from functools import reduce
print(reduce(lambda x, y: x + y, list(map(lambda y: y + 2, x))))

"""
Traceback (most recent call last):
  File "/Users/dgonzales/src/quick-scripts/python/map_and_collect.py", line 8, in <module>
    print(reduce(lambda x: x , list(map(lambda y: y + 2, x))))
TypeError: <lambda>() takes 1 positional argument but 2 were given
print(reduce(lambda x: x , list(map(lambda y: y + 2, x))))
"""
