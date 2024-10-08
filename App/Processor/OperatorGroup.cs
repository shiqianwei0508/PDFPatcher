﻿using System;
using System.Collections.Generic;
using iTextSharp.text.pdf;

namespace PDFPatcher.Processor
{
	internal sealed class OperatorGroup : PdfContentStreamProcessor.IContentOperator
	{
		/// <summary>
		/// 处理内容的 <see cref="PdfContentStreamProcessor.IContentOperator"/> 列表。
		/// </summary>
		public List<PdfContentStreamProcessor.IContentOperator> Operators { get; }
		public bool HasOutput { get; }

		public OperatorGroup(IEnumerable<PdfContentStreamProcessor.IContentOperator> operators) {
			Operators = new List<PdfContentStreamProcessor.IContentOperator>();
			if (operators != null) {
				Operators.AddRange(operators);
				foreach (var item in operators) {
					if (item.HasOutput) {
						HasOutput = true;
						return;
					}
				}
			}
		}

		#region IContentOperator 成员

		public void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands) {
			foreach (var item in Operators) {
				item.Invoke(processor, oper, operands);
			}
		}

		#endregion

	}
}
