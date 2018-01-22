﻿/*
 * SonarLint for Visual Studio
 * Copyright (C) 2016-2018 SonarSource SA
 * mailto:info AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace SonarLint.VisualStudio.Integration.Vsix
{
    [Export(typeof(IQuickInfoSourceProvider))]
    [Name("SonarLint ToolTip Source")]
    [Order(Before = "Default Quick Info Presenter")]
    [ContentType("text")]
    internal class SonarLintQuickInfoSourceProvider : IQuickInfoSourceProvider
    {
        [Import]
        internal ISonarLintDaemon SonarLintDaemon { get; set; }

        [Import]
        internal ITextDocumentFactoryService TextDocumentFactoryService { get; set; }

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            ITextDocument document;
            if (!TextDocumentFactoryService.TryGetTextDocument(textBuffer, out document))
            {
                return null;
            }

            return new SonarLintQuickInfoSource(this, textBuffer.CurrentSnapshot, document?.FilePath);
        }
    }
}