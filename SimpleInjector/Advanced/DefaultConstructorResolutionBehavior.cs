﻿#region Copyright Simple Injector Contributors
/* The Simple Injector is an easy-to-use Inversion of Control library for .NET
 * 
 * Copyright (c) 2013-2016 Simple Injector Contributors
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

namespace SimpleInjector.Advanced
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    [DebuggerDisplay(nameof(DefaultConstructorResolutionBehavior))]
    internal sealed class DefaultConstructorResolutionBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType)
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));

            VerifyTypeIsConcrete(implementationType);

            return GetSinglePublicConstructor(implementationType);
        }

        private static void VerifyTypeIsConcrete(Type implementationType)
        {
            if (!Types.IsConcreteType(implementationType))
            {
                throw new ActivationException(
                    StringResources.TypeShouldBeConcreteToBeUsedOnThisMethod(implementationType));
            }
        }

        private static ConstructorInfo GetSinglePublicConstructor(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();

            if (!constructors.Any())
            {
                throw new ActivationException(
                    StringResources.TypeMustHaveASinglePublicConstructorButItHasNone(implementationType));
            }

            if (constructors.Length > 1)
            {
                throw new ActivationException(
                    StringResources.TypeMustHaveASinglePublicConstructorButItHas(implementationType,
                        constructors.Length));
            }

            return constructors[0];
        }
    }
}