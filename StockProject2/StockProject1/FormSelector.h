#pragma once
#include "aCandlestick.h"
#include "FormMain.h"

namespace StockProject1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Summary for FormSelector
	/// </summary>
	public ref class FormSelector : public System::Windows::Forms::Form
	{
	public:
		FormSelector(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}
		FormSelector(String^ filePath, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			// read candlestick data from the CSV file
			candlestickList = readCandlesticksFromCsv(filePath);
			// filter the candlestick data based on the selected date range
			System::Collections::Generic::List<aCandlestick^>^ filteredData = filterCandlestickData(candlestickList, startDate, endDate);
			//// send the updated list to the DataGridView for display
			//canldestickDataGridView->DataSource = filteredData;
			// Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
			chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aCandlestick^>(filteredData);
			// Refresh the chart to reflect the new data
			chartStockData->DataBind();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~FormSelector()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ buttonSelectFile;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerEndDate;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerStartDate;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialogSelectFile;
	// List to hold candlestick data
	private: List<aCandlestick^>^ candlestickList = gcnew List<aCandlestick^>();
	private: List<aCandlestick^>^ filteredCandlestickList = gcnew List<aCandlestick^>();
	private: System::Windows::Forms::DataVisualization::Charting::Chart^ chartStockData;

	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->buttonSelectFile = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->dateTimePickerEndDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->dateTimePickerStartDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->openFileDialogSelectFile = (gcnew System::Windows::Forms::OpenFileDialog());
			this->SuspendLayout();
			// 
			// buttonSelectFile
			// 
			this->buttonSelectFile->Location = System::Drawing::Point(138, 162);
			this->buttonSelectFile->Name = L"buttonSelectFile";
			this->buttonSelectFile->Size = System::Drawing::Size(100, 34);
			this->buttonSelectFile->TabIndex = 1;
			this->buttonSelectFile->Text = L"Select File";
			this->buttonSelectFile->UseVisualStyleBackColor = true;
			this->buttonSelectFile->Click += gcnew System::EventHandler(this, &FormSelector::buttonSelectFile_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(165, 91);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(52, 13);
			this->label2->TabIndex = 8;
			this->label2->Text = L"End Date";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(161, 23);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(55, 13);
			this->label1->TabIndex = 7;
			this->label1->Text = L"Start Date";
			// 
			// dateTimePickerEndDate
			// 
			this->dateTimePickerEndDate->Location = System::Drawing::Point(91, 115);
			this->dateTimePickerEndDate->Name = L"dateTimePickerEndDate";
			this->dateTimePickerEndDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerEndDate->TabIndex = 6;
			this->dateTimePickerEndDate->Value = System::DateTime(2026, 9, 18, 0, 0, 0, 0);
			// 
			// dateTimePickerStartDate
			// 
			this->dateTimePickerStartDate->Location = System::Drawing::Point(91, 47);
			this->dateTimePickerStartDate->Name = L"dateTimePickerStartDate";
			this->dateTimePickerStartDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerStartDate->TabIndex = 5;
			this->dateTimePickerStartDate->Value = System::DateTime(2026, 6, 1, 0, 0, 0, 0);
			// 
			// openFileDialogSelectFile
			// 
			this->openFileDialogSelectFile->DefaultExt = L"csv";
			this->openFileDialogSelectFile->FileName = L"NVDA_Daily.csv";
			this->openFileDialogSelectFile->Filter = L"All files (*.*)|*.*| Daily | *_daily.csv*| Weekly  | *_weekly.csv| Montly | *_mon"
				L"thly.csv";
			this->openFileDialogSelectFile->InitialDirectory = L"..\\..\\..\\..\\Stock Data";
			this->openFileDialogSelectFile->Multiselect = true;
			this->openFileDialogSelectFile->RestoreDirectory = true;
			this->openFileDialogSelectFile->FileOk += gcnew System::ComponentModel::CancelEventHandler(this, &FormSelector::openFileDialogSelectFile_FileOk);
			// 
			// FormSelector
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(382, 217);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->dateTimePickerEndDate);
			this->Controls->Add(this->dateTimePickerStartDate);
			this->Controls->Add(this->buttonSelectFile);
			this->Name = L"FormSelector";
			this->Text = L"File Selector";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void buttonSelectFile_Click(System::Object^ sender, System::EventArgs^ e) 
	{
		openFileDialogSelectFile->ShowDialog();
	}
		
	private: System::Void openFileDialogSelectFile_FileOk(System::Object^ sender, System::ComponentModel::CancelEventArgs^ e) 
	{
		// get the selected file path
		String^ filePath = openFileDialogSelectFile->FileName;
		// read candlestick data from the CSV file
		candlestickList = readCandlesticksFromCsv(filePath);
		//// display the data in the DataGridView
		//canldestickDataGridView->DataSource = candlestickList;

		// initialize start and end dates from the DateTimePickers
		DateTime startDate = dateTimePickerStartDate->Value;
		DateTime endDate = dateTimePickerEndDate->Value;

		// filter the candlestick data based on the selected date range
		System::Collections::Generic::List<aCandlestick^>^ filteredData = filterCandlestickData(candlestickList, startDate, endDate);
		//// send the updated list to the DataGridView for display
		//canldestickDataGridView->DataSource = filteredData;


		//// Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
		//chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aCandlestick^>(filteredData);
		//// Refresh the chart to reflect the new data
		//chartStockData->DataBind();

		// find the number of files the user selected
		int numFiles = openFileDialogSelectFile->FileNames->Length;

		// for every file the user selected, create a new form and display it
		for (int i = 0; i < numFiles; i++)
		{
			// instantiate a form of type form
			CppCLRWinFormsProject::FormMain^ f = gcnew CppCLRWinFormsProject::FormMain(openFileDialogSelectFile->FileNames[i], startDate, endDate);
			// modify the title at the top of the form to include the file name
			f->Text = "Candlestick Chart - " + Path::GetFileName(openFileDialogSelectFile->FileNames[i]);
			f->Show();
		}
	}

	System::Collections::Generic::List<aCandlestick^>^ StockProject1::FormSelector::readCandlesticksFromCsv(String^ path);
	System::Collections::Generic::List<aCandlestick^>^ StockProject1::FormSelector::filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, DateTime startDate, DateTime endDate);
};
}
