// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class EllipseHandler : ShapeHandler
    {

        public EllipseHandler(NativeComponentRenderer renderer, MCS.Ellipse ellipseControl) : base(renderer, ellipseControl)
        {
            EllipseControl = ellipseControl ?? throw new ArgumentNullException(nameof(ellipseControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Ellipse EllipseControl { get; }
    }
}
