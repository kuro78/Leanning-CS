﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IValueConverterSample
{
    internal class MainViewModel
    {
		public List<FileInfo> Files { get; set; }

		public MainViewModel()
		{
			InitFileList();
		}

		private void InitFileList()
		{
			Files = Directory.GetFiles(Environment.CurrentDirectory).Select(x => new FileInfo(x)).ToList();
		}
	}
}
