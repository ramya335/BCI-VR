#1a

MyString = "python"
index = 0
for i in range(len(MyString)):
    print(MyString[i], end=":")
    if(i == len(MyString)-1):
        print(index)
    else:
        print(index,end=", ")
    index +=1

#1b
print(MyString[2:5])


#1c
print(MyString[::2])

#1d
print(MyString[10:])

#1e
String = "abcdef"
newString = "ghi"

length = len(String)//2
finalString = String[:length]
finalString = finalString+newString
finalString = finalString+String[length:]
print(finalString)

#2a

def common_set(set1,set2):
    count = 0
    for x in set1:
        for y in set2:
            if x == y:
                print(x)
                count +=1
    print("count for ",set1,count)
set1 = {1,2,3,4,5}
set2 = {5,4,6,7,8}
common_set(set1,set2)
set1 = {1,2,3,4,5}
set2 = {6,7,8}
common_set(set1,set2)

#2b

string = "abcdab"

list = [0]*26
output = ""
for i in string:
    value = ord(i) - ord('a')
    if list[value] == 0:
        output = output+i
        list[value] = 1
print(f"unique characters: {output}")


#2c
newList = [13,21,43]
size = len(newList)    
temp = newList[0]
newList[0] = newList[size - 1]
newList[size - 1] = temp 
print(newList)

#2d
list = [2,5,7,2,8,7,5] 
tempList = []

for i in list:
    if i not in tempList:
        tempList.append(i)
list = tempList
print(list)

#2e
dictValue =  {0:"Hello", 1:"Python", 2:"World"} 

for key,value in dictValue.items():
    print(f"key is {key} and value is {value}")

#3a

with open("C:\\Users\\ramya\\Downloads\\Poem2.txt","r") as input:
    with open("C:\\Users\\ramya\\Downloads\\Poem.txt","a") as output:
        for line in input:
            output.write(line)
with open("C:\\Users\\ramya\\Downloads\\Poem.txt") as readFile:
    file = readFile.read()
print(file)

#3b

file = open("C:\\Users\\ramya\\Downloads\\Poem2.txt","r")
data = file.read()
for letter in data:
    if letter == 'T':
        print("#",end="")
    else:
        print(letter,end="")
file.close


#3c
file = open("C:\\Users\\ramya\\Downloads\\Poem2.txt","r")
data = file.read()
words = data.split()
for word in words:
    if len(word)>3:
        print(word,end=" ")
file.close

