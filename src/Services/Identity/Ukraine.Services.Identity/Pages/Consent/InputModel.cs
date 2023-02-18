// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace Ukraine.Services.Identity.Pages.Consent;

public class InputModel
{
	public string? Button { get; set; }
	public IEnumerable<string> ScopesConsented { get; set; } = Array.Empty<string>();
	public bool RememberConsent { get; set; } = true;
	public string? ReturnUrl { get; set; }
	public string? Description { get; set; }
}