#pragma once
#include "aCandlestick.h"

namespace CppCLRWinFormsProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Summary for FormMain
	/// </summary>
	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ buttonSelectFile;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialogSelectFile;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerStartDate;
	private: System::Windows::Forms::DateTimePicker^ dateTimePickerEndDate;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::DataGridView^ canldestickDataGridView;
	private: System::Windows::Forms::Button^ redisplayButton;
	// Lists to hold candlestick data
	private: List<aCandlestick^>^ candlestickList = gcnew List<aCandlestick^>();
	private: List<aCandlestick^>^ filteredCandlestickList = gcnew List<aCandlestick^>();




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
			this->openFileDialogSelectFile = (gcnew System::Windows::Forms::OpenFileDialog());
			this->dateTimePickerStartDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->dateTimePickerEndDate = (gcnew System::Windows::Forms::DateTimePicker());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->canldestickDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->redisplayButton = (gcnew System::Windows::Forms::Button());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->canldestickDataGridView))->BeginInit();
			this->SuspendLayout();
			// 
			// buttonSelectFile
			// 
			this->buttonSelectFile->Location = System::Drawing::Point(261, 429);
			this->buttonSelectFile->Name = L"buttonSelectFile";
			this->buttonSelectFile->Size = System::Drawing::Size(150, 75);
			this->buttonSelectFile->TabIndex = 0;
			this->buttonSelectFile->Text = L"Select File";
			this->buttonSelectFile->UseVisualStyleBackColor = true;
			this->buttonSelectFile->Click += gcnew System::EventHandler(this, &FormMain::buttonSelectFile_Click);
			// 
			// openFileDialogSelectFile
			// 
			this->openFileDialogSelectFile->FileName = L"NVDA_Daily.csv";
			this->openFileDialogSelectFile->Filter = L"All files (*.*)|*.*| Daily | *_daily.csv*| Weekly  | *_weekly.csv| Montly | *_mon"
				L"thly.csv";
			this->openFileDialogSelectFile->InitialDirectory = L"..\\..\\..\\..\\Stock Data";
			this->openFileDialogSelectFile->RestoreDirectory = true;
			this->openFileDialogSelectFile->FileOk += gcnew System::ComponentModel::CancelEventHandler(this, &FormMain::openFileDialog1_FileOk);
			// 
			// dateTimePickerStartDate
			// 
			this->dateTimePickerStartDate->Location = System::Drawing::Point(55, 454);
			this->dateTimePickerStartDate->Name = L"dateTimePickerStartDate";
			this->dateTimePickerStartDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerStartDate->TabIndex = 1;
			this->dateTimePickerStartDate->Value = System::DateTime(2026, 6, 1, 0, 0, 0, 0);
			// 
			// dateTimePickerEndDate
			// 
			this->dateTimePickerEndDate->Location = System::Drawing::Point(567, 454);
			this->dateTimePickerEndDate->Name = L"dateTimePickerEndDate";
			this->dateTimePickerEndDate->Size = System::Drawing::Size(200, 20);
			this->dateTimePickerEndDate->TabIndex = 2;
			this->dateTimePickerEndDate->Value = System::DateTime(2026, 9, 18, 0, 0, 0, 0);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(125, 430);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(55, 13);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Start Date";
			this->label1->Click += gcnew System::EventHandler(this, &FormMain::label1_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(641, 430);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(52, 13);
			this->label2->TabIndex = 4;
			this->label2->Text = L"End Date";
			// 
			// canldestickDataGridView
			// 
			this->canldestickDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->canldestickDataGridView->Location = System::Drawing::Point(1, 1);
			this->canldestickDataGridView->Name = L"canldestickDataGridView";
			this->canldestickDataGridView->Size = System::Drawing::Size(827, 314);
			this->canldestickDataGridView->TabIndex = 5;
			// 
			// redisplayButton
			// 
			this->redisplayButton->Location = System::Drawing::Point(411, 429);
			this->redisplayButton->Name = L"redisplayButton";
			this->redisplayButton->Size = System::Drawing::Size(150, 75);
			this->redisplayButton->TabIndex = 6;
			this->redisplayButton->Text = L"Redisplay";
			this->redisplayButton->UseVisualStyleBackColor = true;
			this->redisplayButton->Click += gcnew System::EventHandler(this, &FormMain::button1_Click);
			// 
			// FormMain
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(831, 509);
			this->Controls->Add(this->redisplayButton);
			this->Controls->Add(this->canldestickDataGridView);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->dateTimePickerEndDate);
			this->Controls->Add(this->dateTimePickerStartDate);
			this->Controls->Add(this->buttonSelectFile);
			this->Name = L"FormMain";
			this->Text = L"FormMain";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->canldestickDataGridView))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		// read candlestick data from a CSV file and display to DataGridView
		private: System::Void openFileDialog1_FileOk(System::Object^ sender, System::ComponentModel::CancelEventArgs^ e)
		{
			// get the selected file path
			String^ filePath = openFileDialogSelectFile->FileName;
			// read candlestick data from the CSV file
			candlestickList = readCandlesticksFromCsv(filePath);
			// display the data in the DataGridView
			canldestickDataGridView->DataSource = candlestickList;
		}

		// show the open file dialog to select a CSV file
		private: System::Void buttonSelectFile_Click(System::Object^ sender, System::EventArgs^ e)
		{
			openFileDialogSelectFile->ShowDialog();
		}

		// mistaken event handler for label click, not used in this application
		private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) 
		{

		}

		// filter the candlestick data based on the selected start and end dates and redisplay
		private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e)
		{
			// initialize start and end dates from the DateTimePickers
			DateTime startDate = dateTimePickerStartDate->Value;
			DateTime endDate = dateTimePickerEndDate->Value;

			// filter the candlestick data based on the selected date range
			System::Collections::Generic::List<aCandlestick^>^ filteredData = filterCandlestickData(candlestickList, startDate, endDate);
			// send the updated list to the DataGridView for display
			canldestickDataGridView->DataSource = filteredData;	
		}

		// function definitions in cpp file
		System::Collections::Generic::List<aCandlestick^>^ CppCLRWinFormsProject::FormMain::readCandlesticksFromCsv(String^ path);
		

		System::Collections::Generic::List<aCandlestick^>^ CppCLRWinFormsProject::FormMain::filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, DateTime startDate, DateTime endDate);
		
	};


}