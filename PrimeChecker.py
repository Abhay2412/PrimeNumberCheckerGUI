# Abhay Khosla
# Prime Number Checker GUI Application
# Project Timeline: August 28th - August 30th
# Location: Zürich, Switzerland 
# Time taken to complete: 8 hours 

# The import statements used in this application  
from functools import partial
from tkinter import *
import tkinter as tk
from  tkinter import ttk
import pandas as pd
from tkinter import filedialog
from tkinter import messagebox


running = True # To check whether the user presses cancel to stop from running the tests 
report_results = [] # The results from manually entering the program 
user_input_from_file = [] # The input from the text file reading 

def isPrime(label_result, numIn): # Implementation of the prime number checker algorithm
    running = True
    if running:
        number1 = (int)(numIn.get()) # Converts the IntVar read from the input box in the GUI application. 
        if number1 > 1: # Checks if the number is not a negative integer 
            for i in range(2, number1//2): # Make sure the number is a prime number 
                if(number1 % i) == 0: # And is not divisible 
                    report_results.append("%d - COMPOSITE" % number1) # Add the results into the report for later parsing by using the dash
                    label_result.config(text="%d - COMPOSITE" % number1) # Update the label in the GUI application
                    break
            else:
                report_results.append("%d - PRIME" % number1)# Add the results into the report for later parsing by using the dash
                label_result.config(text="%d - PRIME" % number1) # Update the label in the GUI application
                return


def isPrimeUpload(numIn): #Similar function to the above the only part is that this does not update the label and is not converting the IntVar here
    running = True
    if running:
        if numIn > 1:
            for i in range(2, numIn//2):
                if(numIn % i) == 0:
                    report_results.append("%d - COMPOSITE" % numIn)
                    break
            else:
                report_results.append("%d - PRIME" % numIn)
                return


def cancelChecking(label_result): #This is the specification approach of cancelling the prime numbers from being tested
    global running
    running = False # Updates the boolean to be False
    label_result.config(text="Cancelled Running Prime Checker")


def saveReport(): # This is converting the list into a data frame for easier output in a tabular format in a text file on the desktop 
    # If the file is not found it will create one. 
    df = pd.DataFrame(report_results, columns=['Number Input', 'Composite/Prime'])
    with open("primeNumberCheckerReport.txt", "w") as f:
        dfAsString = df.to_string(header=True, index=False)
        f.write(dfAsString)


def readFileFromUser(): #This asks the user to upload a file from their local machine with storing the input in a list and then stripping 
    # the new line sign off and converting it to an integer for the next analysis. 
    tf = filedialog.askopenfilename(
        initialdir="C:/Users/MainFrame/Desktop/", 
        title="Open Text file", 
        filetypes=(("Text Files", "*.txt"),)
        )
    tf = open(tf, 'r')
    for line in tf:
        user_input_from_file.append(line.strip())
    for i in range(len(user_input_from_file)):
        user_input_from_file[i] = int(user_input_from_file[i])
        isPrimeUpload(user_input_from_file[i])
    messagebox.showinfo("Information","The results from the text file can be seen in the Report Section of the Program")
    tf.close()

def reportWindow():  # This function invovles the window which creates the table use the Tkinter Python Library
    # This helps gather the data in a nice format. 
    top = Toplevel()
    top.title("Prime Checker - Report")
    top.geometry("500x500")
    report_frame = Frame(top, width=400, height=400)
    report_frame.pack()

    #The scrollbars in the table frame 
    report_scroll = Scrollbar(report_frame)
    report_scroll.pack(side=RIGHT, fill=Y)

    report_scroll = Scrollbar(report_frame,orient='horizontal')
    report_scroll.pack(side= BOTTOM,fill=X)
    
    report = ttk.Treeview(report_frame, yscrollcommand=report_scroll.set, xscrollcommand=report_scroll.set, show='headings', height=8)
    
    report.pack()

    report_scroll.config(command=report.yview)
    report_scroll.config(command=report.xview)

    report['columns'] = ('number_input', 'composite_prime')

    # Format the column by setting a width 
    report.column("#0", width=0,  stretch=NO)
    report.column("number_input", anchor=CENTER, width=80)
    report.column("composite_prime", anchor=CENTER, width=80)

    # Create the headings for the table  
    report.heading("#0", text="", anchor=CENTER)
    report.heading("number_input", text="Number Input", anchor=CENTER)
    report.heading("composite_prime", text="Composite/Prime", anchor=CENTER)

    # Adding the data from the report results collected from manually input
    for i in range(len(report_results)):
        report_results[i] = report_results[i].split("-")
        report.insert(parent='', index='end', iid=i, text='',
                      values=(report_results[i][0], report_results[i][1]))

    report.pack()
    saveButton = tk.Button(top, text="Save Report", command=saveReport)
    saveButton.pack(side = BOTTOM, pady = 20)



canvas_main = tk.Tk() #This is the main window with all of the menu options 
canvas_main.geometry("500x500")
canvas_main.title("Prime Checker - Main")

number1 = tk.IntVar()
#The labels in the window, and their placement 
labelTitle = tk.Label(canvas_main, text="Prime Checker GUI Application").grid(row=0, column=1)
labelNum1 = tk.Label(canvas_main, text="Enter a positive integer number only :").grid(row=1, column=0)
labelResult = tk.Label(canvas_main)
labelResultUpload = tk.Label(canvas_main)
labelResult.grid(row=10, column=1)
labelResultUpload.grid(row=11, column=1)

entryNum1 = tk.Entry(canvas_main, textvariable=number1).grid(row=1, column=1)
isPrime = partial(isPrime, labelResult, number1)
cancelChecking = partial(cancelChecking, labelResult)

#The buttons which are seen in the window and the command is the function which is being called. 
cancelButton = tk.Button(canvas_main, text="Cancel", command=cancelChecking).grid(row=4, column=1)
buttonCheck = tk.Button(canvas_main, text="Check Primality", command=isPrime).grid(row=5, column=1)
accessReportButton = tk.Button(canvas_main, text="Access Report", command=reportWindow).grid(row=6, column=1)
uploadFileButton = tk.Button(canvas_main, text="Upload Text File", command=readFileFromUser).grid(row=7, column=1)

canvas_main.mainloop() #Builds and runs the application 